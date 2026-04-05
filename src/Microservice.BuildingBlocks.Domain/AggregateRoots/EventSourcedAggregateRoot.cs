using Microservice.BuildingBlocks.Domain.Entities;
using Microservice.BuildingBlocks.Domain.Events;

namespace Microservice.BuildingBlocks.Domain.AggregateRoots;

/// <summary>
/// Base class for aggregate root which supports event sourcing.
/// </summary>
public abstract class EventSourcedAggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    private readonly List<IEvent> _changes = [];

    /// <summary>
    /// Gets occurred source of truth events for rebuilding aggregate.
    /// </summary>
    public IReadOnlyCollection<IEvent> Changes => _changes.AsReadOnly();

    /// <summary>
    /// Utilizes for optimistic concurrency control. It should be incremented on every change of aggregate state.
    /// </summary>
    public long Version { get; private set; }

    /// <summary>
    /// Applies event to the aggregate.
    /// <para>It should be implemented in derived classes to change aggregate state on event applying.</para>
    /// </summary>
    /// <param name="event">Event to apply.</param>
    protected abstract void When(IEvent @event);

    /// <summary>
    /// Applies event to the aggregate and adds it to the list of changes.
    /// </summary>
    /// <param name="event">Event to apply and add to the list of changes.</param>
    protected void RaiseEvent(IEvent @event)
    {
        When(@event);
        _changes.Add(@event);
    }

    /// <summary>
    /// Loads aggregate state from history of events.
    /// </summary>
    /// <param name="version">Latest version of aggregate after applying history of events.</param>
    /// <param name="history">History of events for rebuilding aggregate.</param>
    public void LoadFromHistory(long version, IEnumerable<IEvent> history)
    {
        Version = version;
        foreach (var @event in history)
        {
            When(@event);
        }
    }

    /// <summary>
    /// Clear list of changes for current aggregate object.
    /// </summary>
    public void ClearChanges()
    {
        _changes.Clear();
    }
}

/// <summary>
/// Base class for aggregate root that supports Event Sourcing with <see cref="Guid"/> as identifier type which supports domain event handler.
/// </summary>
public abstract class EventSourcedAggregateRoot : Entity<Guid>, IAggregateRoot;
