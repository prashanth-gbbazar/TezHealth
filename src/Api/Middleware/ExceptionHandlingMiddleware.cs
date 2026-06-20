using System.Net;
using System.Text.Json;
using TezHealth.Api.Models.Responses;
using TezHealth.Application.Exceptions;
using ApplicationException = TezHealth.Application.Exceptions.ApplicationException;

namespace TezHealth.Api.Middleware;

/// <summary>
/// Global exception handling middleware for standardized error responses
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
        {
            Timestamp = DateTime.UtcNow,
            TraceId = context.TraceIdentifier
        };

        switch (exception)
        {
            // Check ValidationException first since it inherits from ApplicationException
            case ValidationException validationException:
                response.StatusCode = 400;
                response.Message = validationException.Message;
                response.ErrorCode = validationException.ErrorCode;
                response.Errors = validationException.Errors;
                context.Response.StatusCode = 400;
                break;

            case ApplicationException applicationException:
                response.StatusCode = applicationException.StatusCode;
                response.Message = applicationException.Message;
                response.ErrorCode = applicationException.ErrorCode;
                context.Response.StatusCode = applicationException.StatusCode;
                break;

            case KeyNotFoundException:
                response.StatusCode = 404;
                response.Message = exception.Message;
                response.ErrorCode = "NOT_FOUND";
                context.Response.StatusCode = 404;
                break;

            case ArgumentException argumentException:
                response.StatusCode = 400;
                response.Message = argumentException.Message;
                response.ErrorCode = "INVALID_ARGUMENT";
                context.Response.StatusCode = 400;
                break;

            case InvalidOperationException invalidOperationException:
                response.StatusCode = 400;
                response.Message = invalidOperationException.Message;
                response.ErrorCode = "INVALID_OPERATION";
                context.Response.StatusCode = 400;
                break;

            case UnauthorizedAccessException:
                response.StatusCode = 403;
                response.Message = "You do not have permission to access this resource.";
                response.ErrorCode = "FORBIDDEN";
                context.Response.StatusCode = 403;
                break;

            default:
                response.StatusCode = 500;
                response.Message = "An internal server error occurred. Please try again later.";
                response.ErrorCode = "INTERNAL_SERVER_ERROR";
                context.Response.StatusCode = 500;
                break;
        }

        return context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        }));
    }
}
