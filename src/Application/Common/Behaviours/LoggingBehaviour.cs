using RedPhase.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace RedPhase.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService, IIdentityService identityService)
    {
        this._logger = logger;
        this._currentUserService = currentUserService;
        this._identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = this._currentUserService.UserId ?? string.Empty;
        string userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            userName = await this._identityService.GetUserNameAsync(userId);
        }

        this._logger.LogInformation("RedPhase Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }
}
