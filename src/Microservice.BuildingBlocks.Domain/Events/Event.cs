namespace Microservice.BuildingBlocks.Domain.Events;

/// <summary>
/// Event base class for Event Sourcing, that stores information about the event, which has been occurred on.
/// </summary>
public abstract record Event : DomainEvent, IEvent
{
    /// <inheritdoc/>
    public Guid AggregateId { get; init; }

    /// <inheritdoc/>
    public long Version { get; init; }
}
