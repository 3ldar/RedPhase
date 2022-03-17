using RedPhase.Domain.Common;

namespace RedPhase.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}
