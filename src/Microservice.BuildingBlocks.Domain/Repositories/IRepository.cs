using Microservice.BuildingBlocks.Domain.AggregateRoots;
using Microservice.BuildingBlocks.Domain.UnitOfWorks;

namespace Microservice.BuildingBlocks.Domain.Repositories;

public interface IRepository<T, TId>
    where T : IAggregateRoot
    where TId : notnull
{
    IUnitOfWork UnitOfWork { get; }
    Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task AddAsync(T aggregate, CancellationToken cancellationToken = default);
    void Update(T aggregate);
    void Remove(T aggregate);
}
