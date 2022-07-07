using RedPhase.Application.Common.Models;
using RedPhase.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RedPhase.Application.TodoItems.EventHandlers;

public class TodoItemCompletedEventHandler : INotificationHandler<DomainEventNotification<TodoItemCompletedEvent>>
{
    private readonly ILogger<TodoItemCompletedEventHandler> _logger;

    public TodoItemCompletedEventHandler(ILogger<TodoItemCompletedEventHandler> logger)
    {
        this._logger = logger;
    }

    public Task Handle(DomainEventNotification<TodoItemCompletedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        this._logger.LogInformation("RedPhase Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}
