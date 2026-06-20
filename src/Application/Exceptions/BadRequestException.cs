namespace TezHealth.Application.Exceptions;

/// <summary>
/// Exception thrown when a request contains invalid or malformed data
/// </summary>
public class BadRequestException : ApplicationException
{
    public BadRequestException(string message, string? errorCode = "BAD_REQUEST")
        : base(message, 400, errorCode)
    {
    }
}
