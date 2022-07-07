using RedPhase.Application.Common.Models;
using RedPhase.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RedPhase.Application.TodoItems.EventHandlers;

public class TodoItemCreatedEventHandler : INotificationHandler<DomainEventNotification<TodoItemCreatedEvent>>
{
    private readonly ILogger<TodoItemCreatedEventHandler> _logger;

    public TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger)
    {
        this._logger = logger;
    }

    public Task Handle(DomainEventNotification<TodoItemCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;

        this._logger.LogInformation("RedPhase Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        return Task.CompletedTask;
    }
}
