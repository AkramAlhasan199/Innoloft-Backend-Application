using Innoloft_Backend_Domain.Entities;
using Innoloft_Backend_Domain.Repositories;
using Innoloft_Backend_Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Domain.Services
{
    public abstract class CrudService<TEntity> : CrudService<TEntity, string, object>
        where TEntity : Entity
    {
        protected CrudService(IRepository<TEntity, string> repository) :
            base(repository)
        {
        }
    }

    public abstract class CrudService<TEntity, TKey, TRecord> : ICrudService<TEntity, TKey>
        where TEntity : Entity
        where TKey : IEquatable<TKey>
    {
        protected CrudService(IRepository<TEntity, TKey> repository)
        {
            Repository = repository;
            UnitOfWork = Repository.UnitOfWork;
        }

        protected readonly IRepository<TEntity, TKey> Repository;

        protected readonly IUnitOfWork UnitOfWork;

        public virtual async Task<GResult<TEntity>> CreateAsync(TEntity entity)
        {
            entity.CreatedOn = DateTime.UtcNow;
            await Repository.AddAsync(entity);
            await UnitOfWork.SaveChangesAsync();
            return GResult<TEntity>.Success(entity);
        }

        public virtual async Task<GResult<TEntity>> DeleteAsync(TEntity entity)
        {
            await Repository.DeleteAsync(entity);
            try
            {
                await UnitOfWork.SaveChangesAsync();
                return GResult<TEntity>.Success(entity);
            }
            catch (Exception)
            {
                return GResult<TEntity>.Failed("Unknow Error");
            }
        }

        public virtual async Task<GResult<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> spec)
        {
            var result = await Repository.GetAsync(spec);
            return result != null
                ? GResult<TEntity>.Success(result)
                : GResult<TEntity>.Failed("Not Found");
        }

        public virtual async Task<GResult<TResult>> ReadAsync<TResult>(Expression<Func<TEntity, bool>> spec,
            Expression<Func<TEntity, TResult>> selector)
        {
            var result = await Repository.GetAsync(spec, selector);
            return result != null
                ? GResult<TResult>.Success(result)
                : GResult<TResult>.Failed("Not Found");
        }

        public virtual async Task<GResult<PagedList<TResult>>> ReadListAsync<TResult>(DataSourceRequest pageRequest, Expression<Func<TEntity, bool>> spec,
            Expression<Func<TEntity, TResult>> selector)
        {
            var result = await Repository.GetListAsync(pageRequest, spec, selector);
            return result != null
                ? GResult<PagedList<TResult>>.Success(result)
                : GResult<PagedList<TResult>>.Failed("Not Found");
        }

        public virtual async Task<GResult<TEntity>> UpdateAsync(TEntity entity)
        {
            entity.UpdatedOn = DateTime.UtcNow;
            await Repository.UpdateAsync(entity);
            await UnitOfWork.SaveChangesAsync();
            return GResult<TEntity>.Success(entity);
        }
    }
}
