namespace ProcessMemory.windows;

/// <summary>
/// Represents available process access.
/// </summary>
[Flags]
public enum ProcessAccess : uint
{
    VirtualMemoryRead = 0x00000010
}