using Ardalis.GuardClauses;
using Ardalis.Result;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PaperSquare.Core.Application.Shared.Dto;
using PaperSquare.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperSquare.Infrastructure.Shared
{
    public class CRUDService<TEntity, TModel, TType, TSearch, TInsert, TUpdate>: 
                    QueryService<TEntity, TType, TModel, TSearch>, 
                    ICommandService<TModel, TSearch, TType, TInsert, TUpdate> 
                    where TEntity: class where TModel : class where TSearch : SearchRequest where TInsert : class where TUpdate : class
    {
        public CRUDService(PaperSquareDbContext dbContext, IMapper mapper): base(dbContext, mapper){}
               
        public virtual async Task<Result<TModel>> Insert(TInsert insert)
        {
            Guard.Against.Null(insert, nameof(insert));

            var entity = _mapper.Map<TEntity>(insert);

            _entities.Add(entity);

            await _dbContext.SaveChangesAsync();

            return Result.Success(_mapper.Map<TModel>(entity), $"{typeof(TEntity).Name} successfully added!");
        }

        public virtual async Task<Result<TModel>> Update(TType type, TUpdate update)
        {
            Guard.Against.Null(update, nameof(update));
            Guard.Against.Null(type, nameof(type));

            var entity = await _entities.FindAsync(type);

            if (entity is null)
            {
                throw new NotFoundException($"Entity with id: {type} not found!", typeof(TEntity).Name);
            }

            _mapper.Map(update, entity);

            await _dbContext.SaveChangesAsync();

            return Result.Success(_mapper.Map<TModel>(entity), $"{typeof(TEntity).Name} successfully updated!");
        }

        public virtual async Task<Result<TModel>> Delete(TType type)
        {
            // TO DO: Check for user permissions for entity deleteion

            var entity = await _entities.FindAsync(type);

            if (entity is null)
            {
                throw new NotFoundException($"Entity with id: {type} not found!", typeof(TEntity).Name);
            }

            _entities.Remove(entity);

            await _dbContext.SaveChangesAsync();

            return Result.Success(_mapper.Map<TModel>(entity));
        }

    }
}
