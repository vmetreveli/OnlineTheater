using Microsoft.Extensions.Logging;
using Referendum.Application;

namespace OnlineTheater.Applications.Behaviours;

public sealed class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger) =>
        _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex,
                "{AssemblyReferenceName} Request: Unhandled Exception for Request {RequestName} {Request}",
                nameof(AssemblyReference), requestName, request);

            throw;
        }
    }
}