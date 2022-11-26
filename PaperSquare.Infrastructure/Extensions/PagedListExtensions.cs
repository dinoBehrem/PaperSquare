using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Extensions
{
    public static class PagedListExtensions
    {
        public static IQueryable ToPagedList<TEnitity>(this IQueryable<TEnitity> query, int page, int pageSize)
        {
            query = query.Take(pageSize).Skip((page - 1) * pageSize);

            return query;
        }
    }
}
