using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ProcessMemory.windows;

/// <summary>
/// Represents a process wrapper.
/// </summary>
public class ProcessWrapper : IProcessWrapper
{
    /// <summary>
    /// The handle process.
    /// </summary>
    private readonly Process _process;

    /// <summary>
    /// A pointer to the handle process.
    /// </summary>
    public nint Handle { get; }

    /// <summary>
    /// Create a new wrapper on the specified process.
    /// </summary>
    /// <param name="processName">The name of process to wrap.</param>
    /// <exception cref="ArgumentException">If the process with the specified name cannot be found.</exception>
    public ProcessWrapper(string processName)
    {
        var process = Process.GetProcessesByName(processName).FirstOrDefault();

        _process = process ?? throw new WindowsAPIException($"Process {processName} not found.");

        Handle = PInvoke.OpenProcess(ProcessAccess.VirtualMemoryRead, false, _process.Id);
    }

    /// <summary>
    /// Read the address pointed by the specified multi level pointer.
    /// </summary>
    /// <param name="multiLevelPointer">The multi level pointer to read</param>
    /// <returns>The pointed address</returns>
    public nint? ReadMultiLevelPointer(MultiLevelPointer multiLevelPointer)
    {
        var baseAddress = (nint) _process.MainModule!.BaseAddress.ToInt64() + multiLevelPointer.BaseAddress;
        var nextAddress = Read<long>(baseAddress);

        if (!nextAddress.HasValue)
        {
            return null;
        }

        if (ReadMultiLevelPointerOffsets(multiLevelPointer, ref nextAddress)) return null;

        return (nint?) nextAddress;
    }

    /// <summary>
    /// Read a value at the specified address.
    /// </summary>
    /// <param name="address">The address that contains the value to read</param>
    /// <typeparam name="T">The type of value to read</typeparam>
    /// <returns>The read value</returns>
    public unsafe T? Read<T>(nint address) where T : unmanaged
    {
        var buffer = stackalloc T[1];

        var success = PInvoke.ReadProcessMemory(Handle, address, buffer, sizeof(T), out _);

        return success ? buffer[0] : null;
    }

    /// <summary>
    /// Read the offsets of the specified multi level pointer.
    /// </summary>
    /// <param name="multiLevelPointer">The multi level pointer to read</param>
    /// <param name="nextAddress">The address to update when an offset has been read</param>
    /// <returns>TRUE if read successfully, FALSE otherwise</returns>
    private bool ReadMultiLevelPointerOffsets(MultiLevelPointer multiLevelPointer, [DisallowNull] ref long? nextAddress)
    {
        for (var i = 0; i < multiLevelPointer.Offsets.Length; i++)
        {
            if (i == multiLevelPointer.Offsets.Length - 1)
            {
                nextAddress += multiLevelPointer.Offsets.ElementAt(i);
            }
            else
            {
                nextAddress = Read<long>((nint) (nextAddress + multiLevelPointer.Offsets.ElementAt(i)));

                if (!nextAddress.HasValue)
                {
                    return true;
                }
            }
        }

        return false;
    }
}