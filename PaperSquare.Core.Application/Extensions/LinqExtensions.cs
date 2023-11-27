using System.Linq.Expressions;

namespace PaperSquare.Infrastructure.Data.Extensions;

internal static class LinqExtensions
{
    internal static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> query, bool condition, Expression<Func<TEntity, bool>> func)
    {
        if(condition)
        {
            query = query.Where(func);
        }

        return query;
    }
}
