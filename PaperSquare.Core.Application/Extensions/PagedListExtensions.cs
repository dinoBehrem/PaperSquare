namespace PaperSquare.Core.Application.Extensions;

public static class PagedListExtensions
{
    public static IQueryable<TEnitity> ToPagedList<TEnitity>(this IQueryable<TEnitity> query, int page, int pageSize)
    {
        query = query.Take(pageSize).Skip((page - 1) * pageSize);

        return query;
    }
}
