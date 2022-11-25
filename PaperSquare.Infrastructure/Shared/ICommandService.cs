using Ardalis.Result;
using PaperSquare.Infrastructure.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Shared
{
    public interface ICommandService<TModel, TSearch, TType, TInsert, TUpdate> : IQueryService<TModel, TSearch, TType> where TModel : class where TSearch : SearchDto where TInsert : class where TUpdate : class
    {
        Task<Result<TModel>> Insert(TInsert insert);
        Task<Result<TModel>> Update(TType type, TUpdate update);
        Task<Result<TModel>> Delete(TType type);
    }
}
