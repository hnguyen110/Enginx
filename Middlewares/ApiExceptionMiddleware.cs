using System.Net;
using API.Exceptions;
using Newtonsoft.Json;

namespace API.Middlewares;

public class ApiExceptionMiddleware
{
    private readonly ILogger<ApiExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;
    private HttpStatusCode _code;
    private string? _message;

    public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            await HandleException(httpContext, exception, _logger);
        }
    }

    private async Task HandleException(HttpContext httpContext, Exception exception, ILogger logger)
    {
        switch (exception)
        {
            case ApiException error:
                _code = error.Code;
                _message = error.ExceptionMessage;
                break;
            default:
                _code = HttpStatusCode.InternalServerError;
                _message = exception.Message;
                break;
        }

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)_code;
        var result = JsonConvert.SerializeObject(new
        {
            message = _message
        });
        await httpContext.Response.WriteAsync(result);
    }
}