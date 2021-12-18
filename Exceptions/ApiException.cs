using System.Net;

namespace API.Exceptions;

public class ApiException : Exception
{
    public ApiException(HttpStatusCode code, string? message)
    {
        Code = code;
        ExceptionMessage = message;
    }

    public HttpStatusCode Code { get; }

    public string? ExceptionMessage { get; }
}