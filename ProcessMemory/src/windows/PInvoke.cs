using System.Runtime.InteropServices;

namespace ProcessMemory.windows;

/// <summary>
/// Contains all PInvoke methods.
/// </summary>
internal static partial class PInvoke
{
    /// <summary>
    /// A wrapper on OpenProcess method come from Windows API :
    /// https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openprocess
    /// </summary>
    /// <param name="dwDesiredAccess"></param>
    /// <param name="bInheritHandle"></param>
    /// <param name="processId"></param>
    /// <returns></returns>
    [LibraryImport("kernel32.dll")]
    internal static partial nint OpenProcess(
        ProcessAccess dwDesiredAccess,
        [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
        int processId
    );

    /// <summary>
    /// A wrapper on ReadProcessMemory method come from Windows API :
    /// https://learn.microsoft.com/en-us/windows/win32/api/memoryapi/nf-memoryapi-readprocessmemory
    /// </summary>
    /// <param name="hProcess"></param>
    /// <param name="lpBaseAddress"></param>
    /// <param name="lpBuffer"></param>
    /// <param name="dwSize"></param>
    /// <param name="lpNumberOfBytesRead"></param>
    /// <returns></returns>
    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static unsafe partial bool ReadProcessMemory(
        nint hProcess,
        nint lpBaseAddress,
        void* lpBuffer,
        int dwSize,
        out int lpNumberOfBytesRead
    );

    /// <summary>
    /// A wrapper on GetLastError method come from Windows API :
    /// https://learn.microsoft.com/en-us/windows/win32/api/errhandlingapi/nf-errhandlingapi-getlasterror
    /// </summary>
    /// <returns></returns>
    [LibraryImport("kernel32.dll")]
    internal static partial int GetLastError();
}