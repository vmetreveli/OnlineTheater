using System.Net;
using System.Text.Json;
using ErrorOr;
using Referendum.Application.Exceptions;

namespace OnlineTheater.Api.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private const string MESSAGE_TEMPLATE =
        "Source : {ExceptionHandlingMiddleware}, URL : {Url},  Error :  {Exception}, Inner Exception, {InnerException}";

    private readonly ILogger _logger;
    // private readonly RequestDelegate _next;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ExceptionHandlingMiddleware" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public ExceptionHandlingMiddleware(ILoggerFactory logger) =>
        _logger = logger.CreateLogger(nameof(ExceptionHandlingMiddleware));

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, MESSAGE_TEMPLATE, nameof(ExceptionHandlingMiddleware),
                context.Connection.ClientCertificate,
                ex.Message, ex.InnerException);


            await HandleExceptionAsync(context, ex);
        }
    }


    /// <summary>
    ///     Handles the specified <see cref="Exception" /> for the specified <see cref="HttpContext" />.
    /// </summary>
    /// <param name="httpContext">The HTTP httpContext.</param>
    /// <param name="exception">The exception.</param>
    /// <returns>The HTTP response that is modified based on the exception.</returns>
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var (httpStatusCode, errors) = GetHttpStatusCodeAndErrors(exception);

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = (int) httpStatusCode;

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var response = JsonSerializer.Serialize(errors, serializerOptions);

        await httpContext.Response.WriteAsync(response);
    }

    /// <summary>
    ///     Extracts the HTTP status code and a collection of errors based on the specified exception.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <returns>The HTTP status code and a collection of errors based on the specified exception.</returns>
    private static (HttpStatusCode HttpStatusCode, IReadOnlyCollection<Error> Errors) GetHttpStatusCodeAndErrors(
        Exception exception) =>
        exception switch
        {
            ValidationException validationException => ( HttpStatusCode.BadRequest, validationException.Errors ),
            _ => ( HttpStatusCode.InternalServerError, new[] {Error.Failure(description:exception.Message)} )
        };
}