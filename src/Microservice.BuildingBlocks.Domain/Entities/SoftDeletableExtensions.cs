namespace Microservice.BuildingBlocks.Domain.Entities;

public static class SoftDeletableExtensions
{
    public static IEnumerable<T> WhereNotDeleted<T>(this IEnumerable<T> source) where T : ISoftDeletable
    {
        return source.Where(x => !x.IsDeleted);
    }

    public static IQueryable<T> WhereNotDeleted<T>(this IQueryable<T> source) where T : ISoftDeletable
    {
        return source.Where(x => !x.IsDeleted);
    }
}
