namespace TezHealth.Application.Exceptions;

/// <summary>
/// Exception thrown when a resource already exists
/// </summary>
public class ConflictException : ApplicationException
{
    public ConflictException(string message, string? errorCode = "CONFLICT")
        : base(message, 409, errorCode)
    {
    }
}
