namespace ProcessMemory;

/// <summary>
/// The interface for all process wrapper.
/// </summary>
public interface IProcessWrapper
{
    /// <summary>
    /// Get a pointer on the handle process.
    /// </summary>
    nint Handle { get; }

    /// <summary>
    /// Read an int value at the specified address.
    /// </summary>
    /// <param name="address">The address to read</param>
    /// <returns>The int read</returns>
    // int ReadInt(nint address);
}