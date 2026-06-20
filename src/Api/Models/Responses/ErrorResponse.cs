namespace TezHealth.Api.Models.Responses;

/// <summary>
/// Standard error response model
/// </summary>
public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? ErrorCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? TraceId { get; set; }
    public IDictionary<string, string[]>? Errors { get; set; }

    public ErrorResponse()
    {
    }

    public ErrorResponse(int statusCode, string message, string? errorCode = null, string? traceId = null)
    {
        StatusCode = statusCode;
        Message = message;
        ErrorCode = errorCode;
        TraceId = traceId;
    }
}
