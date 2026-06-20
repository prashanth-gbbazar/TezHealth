namespace TezHealth.Application.Exceptions;

/// <summary>
/// Exception thrown when a request is forbidden
/// </summary>
public class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message, string? errorCode = "FORBIDDEN")
        : base(message, 403, errorCode)
    {
    }
}
