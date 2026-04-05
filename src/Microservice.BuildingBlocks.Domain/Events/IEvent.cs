namespace Microservice.BuildingBlocks.Domain.Events;

/// <summary>
/// Event abstraction for Event Sourcing, that stores information about the event, which has been occurred on.
/// </summary>
public interface IEvent : IDomainEvent
{
    /// <summary>
    /// The identifier of the aggregate which has generated the event
    /// </summary>
    Guid AggregateId { get; }

    /// <summary>
    /// The version of the aggregate when the event has been generated
    /// </summary>
    long Version { get; }
}
