using Ardalis.Result;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Application.Shared.Dto;
using PaperSquare.Data.Data;
using PaperSquare.Infrastructure.Exceptions;
using PaperSquare.Infrastructure.Extensions;

namespace PaperSquare.Infrastructure.Shared
{
    public class QueryService<TEntity, TType, TModel, TSearch> : IQueryService<TModel, TSearch, TType> where TEntity : class where TModel : class where TSearch : SearchRequest
    {
        protected readonly PaperSquareDbContext _dbContext;
        protected readonly IMapper _mapper;
        protected DbSet<TEntity> _entities => _dbContext.Set<TEntity>();

        public QueryService(PaperSquareDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public virtual async Task<Result<IEnumerable<TModel>>> GetAll(TSearch search = null)
        {
            var entities = ApplyFilters(_entities, search);

            var pagedEntities = _mapper.Map<IEnumerable<TModel>>(entities.ToPagedList(search.page.Value, search.pageSize.Value));

            return Result.Success(pagedEntities);
        }

        public virtual async Task<Result<TModel>> GetById(TType id)
        {
            var entity = await _entities.FindAsync(id);

            if (entity is null)
            {
                throw new NotFoundEntityException($"{typeof(TEntity).Name} not found!", typeof(TEntity));
            }

            var mappedEntity = _mapper.Map<TModel>(entity);

            return Result.Success(mappedEntity);
        }

        #region Utils

        public virtual IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> query, TSearch search = null)
        {
            return query;
        }

        #endregion Utils
    }
}
