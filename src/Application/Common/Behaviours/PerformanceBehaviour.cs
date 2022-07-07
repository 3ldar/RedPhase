using System.Diagnostics;
using RedPhase.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RedPhase.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public PerformanceBehaviour(
        ILogger<TRequest> logger,
        ICurrentUserService currentUserService,
        IIdentityService identityService)
    {
        this._timer = new Stopwatch();

        this._logger = logger;
        this._currentUserService = currentUserService;
        this._identityService = identityService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        this._timer.Start();

        var response = await next();

        this._timer.Stop();

        var elapsedMilliseconds = this._timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            var userId = this._currentUserService.UserId ?? string.Empty;
            var userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await this._identityService.GetUserNameAsync(userId);
            }

            this._logger.LogWarning("RedPhase Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);
        }

        return response;
    }
}
