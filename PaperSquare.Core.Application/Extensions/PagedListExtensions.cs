using System.Collections;

namespace PaperSquare.Infrastructure.Extensions;

public static class PagedListExtensions
{
    public static IEnumerable<TEnitity> ToPagedList<TEnitity>(this IEnumerable<TEnitity> query, int page, int pageSize)
    {
        query = query.Take(pageSize).Skip((page - 1) * pageSize);

        return query;
    }
}
