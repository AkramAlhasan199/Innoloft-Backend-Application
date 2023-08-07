using Innoloft_Backend_Domain.Entities;
using Innoloft_Backend_Domain.Repositories;
using Innoloft_Backend_Domain.Utils;
using Innoloft_Backend_Persistence.Contexts;
using Innoloft_Backend_Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Persistence.Repositories
{
    public class Repository<TEntity> : Repository<TEntity, string>
       where TEntity : Entity<string>
    {
        public Repository(AppDbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {
        }
    }
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
        where TKey : IEquatable<TKey>

    {
        protected readonly AppDbContext DbContext;

        public IUnitOfWork UnitOfWork { get; }

        public Repository(AppDbContext dbContext, IUnitOfWork unitOfWork)
        {
            DbContext = dbContext;
            UnitOfWork = unitOfWork;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await DbContext.AddAsync<TEntity>(entity);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            DbContext.Remove<TEntity>(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            DbContext.Update<TEntity>(entity);
            return Task.CompletedTask;
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> spec)
        {
            var result = await DbContext.Set<TEntity>().FirstOrDefaultAsync(spec);

            return result;
        }

        public virtual async Task<TResult> GetAsync<TResult>(Expression<Func<TEntity, bool>> spec,
            Expression<Func<TEntity, TResult>> selector)
        {
            var result = await DbContext.Set<TEntity>().Where(spec).Select(selector).FirstOrDefaultAsync();
            return result;
        }

        public virtual async Task<PagedList<TResult>> GetListAsync<TResult>(DataSourceRequest pageRequest, Expression<Func<TEntity, bool>> spec,
            Expression<Func<TEntity, TResult>> selector)
        {
            var totalCount = DbContext.Set<TEntity>().Where(spec).Count();

            var result = await DbContext.Set<TEntity>().Where(spec).Select(selector)
                .Skip(pageRequest.PageSize * pageRequest.Page)
                .Take(pageRequest.PageSize)
                .ToListAsync();

            return result.ToPagedList(totalCount, pageRequest.Page, pageRequest.PageSize);
        }

    }
}
