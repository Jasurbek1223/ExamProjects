using AirBnb.Api.Common.Filtering;

namespace AirBnb.Api.Common.Quering;

public static class LinqExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> source, FilterPagination filterPagination)
    {
        return source
            .Skip((filterPagination.PageToken - 1) * filterPagination.PageSize)
            .Take(filterPagination.PageSize);
    }
}