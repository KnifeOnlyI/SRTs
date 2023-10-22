namespace ProcessMemory;

/// <summary>
/// Represent a multi level pointer.
/// </summary>
public class MultiLevelPointer
{
    /// <summary>
    /// The base address.
    /// </summary>
    public nint BaseAddress { get; }

    /// <summary>
    /// The list of offsets.
    /// </summary>
    public long[] Offsets { get; }

    /// <summary>
    /// Create a new multi level pointer.
    /// </summary>
    /// <param name="baseAddress">The base address</param>
    /// <param name="offsets">The list of offsets</param>
    public MultiLevelPointer(nint baseAddress, long[] offsets)
    {
        BaseAddress = baseAddress;
        Offsets = offsets;
    }
}