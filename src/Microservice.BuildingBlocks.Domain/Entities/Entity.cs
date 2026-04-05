using Microservice.BuildingBlocks.Domain.BusinessRules;
using Microservice.BuildingBlocks.Domain.Events;

namespace Microservice.BuildingBlocks.Domain.Entities;

public abstract class Entity<TId> : IEntity<TId>
{
    /// <summary>
    /// Gets entity identifier.
    /// </summary>
    public virtual TId Id { get; protected set; } = default!;
    object IEntity.Id => Id!;

    /// <summary>
    /// Checks business rules.
    /// </summary>
    /// <param name="rule">Business rule to check.</param>
    /// <exception cref="BusinessRuleValidationException">Exception can be thrown on invalid business rule.</exception>
    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.BrokenWhen)
        {
            throw new BusinessRuleValidationException(rule);
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other) return false;
        if (ReferenceEquals(this, other)) return true;
        if (IsTransient() || other.IsTransient()) return false;
        return Id!.Equals(other.Id);
    }

    public bool IsTransient() => Equals(Id, default(TId));

    /// <summary>
    /// <see href="http://www.reflectivesoftware.com/2015/07/26/unit-of-work-entity-framework-unity-ddd-hexagonal-onion/">Read more about XOR for random distribution</see>.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode() => IsTransient() ? base.GetHashCode() : Id!.GetHashCode() ^ 31;

    public static bool operator ==(Entity<TId> left, Entity<TId> right) => Equals(left, right);
    public static bool operator !=(Entity<TId> left, Entity<TId> right) => !Equals(left, right);
}

/// <summary>
/// Base class for entity with <see cref="Guid"/> as identifier type.
/// </summary>
public abstract class Entity : Entity<Guid>;
