namespace TezHealth.Application.Exceptions;

/// <summary>
/// Base exception class for application-specific exceptions
/// </summary>
public class ApplicationException : Exception
{
    public int StatusCode { get; set; }
    public string? ErrorCode { get; set; }

    public ApplicationException(string message, int statusCode = 500, string? errorCode = null)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }
}
