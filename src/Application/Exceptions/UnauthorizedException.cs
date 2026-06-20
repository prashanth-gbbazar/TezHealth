namespace TezHealth.Application.Exceptions;

/// <summary>
/// Exception thrown when the user is not authenticated
/// </summary>
public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message = "Unauthorized access.", string? errorCode = "UNAUTHORIZED")
        : base(message, 401, errorCode)
    {
    }
}
