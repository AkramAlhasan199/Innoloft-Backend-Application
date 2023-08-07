using Innoloft_Backend_Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Domain.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, string>
    {
    }

    public interface IRepository<TEntity, TKey>
        where TKey : IEquatable<TKey>
    {
        IUnitOfWork UnitOfWork { get; }

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> spec);

        Task<TResult> GetAsync<TResult>(Expression<Func<TEntity, bool>> spec,
            Expression<Func<TEntity, TResult>> selector);

        Task<PagedList<TResult>> GetListAsync<TResult>(DataSourceRequest pageRequest, Expression<Func<TEntity, bool>> spec,
            Expression<Func<TEntity, TResult>> selector);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

    }
}
