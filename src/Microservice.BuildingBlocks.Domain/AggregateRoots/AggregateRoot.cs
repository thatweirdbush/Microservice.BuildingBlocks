using Microservice.BuildingBlocks.Domain.Entities;
using Microservice.BuildingBlocks.Domain.Events;

namespace Microservice.BuildingBlocks.Domain.AggregateRoots;

/// <summary>
/// Base class for aggregate root which supports domain event handler.
/// </summary>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = [];

    /// <summary>
    /// Gets occurred domain events.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Add domain event to the collection of domain events for current entity.
    /// </summary>
    /// <param name="domainEvent">Occurred domain event.</param>
    /// <exception cref="ArgumentNullException">The domain event must not be empty.</exception>
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        ArgumentNullException.ThrowIfNull($"The domain event '{domainEvent}' must not be empty.");
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// Clear list of domain events for current entity object.
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

/// <summary>
/// Base class for aggregate root with <see cref="Guid"/> as identifier type which supports domain event handler.
/// </summary>
public abstract class AggregateRoot : AggregateRoot<Guid>, IAggregateRoot;
