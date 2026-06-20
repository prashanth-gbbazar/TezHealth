namespace TezHealth.Api.Models.Responses;

/// <summary>
/// Success response wrapper for API responses
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? TraceId { get; set; }

    public ApiResponse()
    {
    }

    public ApiResponse(T data, string message = "Success", string? traceId = null)
    {
        Success = true;
        Data = data;
        Message = message;
        TraceId = traceId;
    }

    public static ApiResponse<T> SuccessResponse(T data, string message = "Success", string? traceId = null)
    {
        return new ApiResponse<T>(data, message, traceId);
    }
}

/// <summary>
/// Non-generic success response
/// </summary>
public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? TraceId { get; set; }

    public ApiResponse()
    {
    }

    public ApiResponse(string message = "Success", string? traceId = null)
    {
        Success = true;
        Message = message;
        TraceId = traceId;
    }

    public static ApiResponse SuccessResponse(string message = "Success", string? traceId = null)
    {
        return new ApiResponse(message, traceId);
    }
}
