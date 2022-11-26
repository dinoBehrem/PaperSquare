using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Shared
{
    public interface IQueryService<TModel, TSearch, TType> where TModel : class where TSearch : class
    {
        Task<Result<IEnumerable<TModel>>> GetAll(TSearch search = null);
        Task<Result<TModel>> GetById(TType id);
    }
}
