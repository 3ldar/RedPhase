using RedPhase.Domain.Common;
using MediatR;

namespace RedPhase.Application.Common.Models;

public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
{
    public DomainEventNotification(TDomainEvent domainEvent)
    {
        this.DomainEvent = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}
