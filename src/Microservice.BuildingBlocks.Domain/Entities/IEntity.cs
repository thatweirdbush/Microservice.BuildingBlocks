namespace Microservice.BuildingBlocks.Domain.Entities;

/// <summary>
/// Interface for DDD entity pattern.
/// </summary>
public interface IEntity
{
    object Id { get; }
}

/// <summary>
/// Interface for DDD entity pattern that supports Covariance &amp; Generic Casting.
/// </summary>
public interface IEntity<out TId> : IEntity
{
    new TId Id { get; }
}
