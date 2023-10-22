namespace ProcessMemory.windows;

/// <summary>
/// The exception for all windows API.
/// </summary>
public class WindowsAPIException : Exception
{
    /// <summary>
    /// Create a new exception with the specified message.
    /// </summary>
    /// <param name="message">The message</param>
    public WindowsAPIException(string message) : base(message)
    {
    }

    /// <summary>
    /// Create a new exception with the specified error flag.
    /// </summary>
    /// <param name="errorFlag">The error flag</param>
    public WindowsAPIException(int errorFlag) : base($"Error flag: {errorFlag}")
    {
    }
}