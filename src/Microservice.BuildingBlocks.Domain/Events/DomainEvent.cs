namespace Microservice.BuildingBlocks.Domain.Events;

/// <summary>
/// Domain event abstraction, that stores information, when the event was occurred on.
/// </summary>
public abstract record DomainEvent : IDomainEvent
{
    /// <inheritdoc/>
    public Guid EventId { get; } = Guid.NewGuid();

    /// <inheritdoc/>
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}
