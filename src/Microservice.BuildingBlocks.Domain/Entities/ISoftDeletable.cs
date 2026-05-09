namespace Microservice.BuildingBlocks.Domain.Entities;

/// <summary>
/// Defines standardized properties for soft-deletable entities.
/// </summary>
public interface ISoftDeletable
{
    bool IsDeleted { get; }
    DateTime? DeletedAt { get; }
}
