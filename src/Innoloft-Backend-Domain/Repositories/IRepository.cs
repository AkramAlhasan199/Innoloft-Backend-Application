using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Domain.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, string>
         //where TEntity : IHasKey<string>
    {
    }

    public interface IRepository<TEntity, TKey>
        //where TEntity : IHasKey<TKey>
        where TKey : IEquatable<TKey>
    {

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> spec);

        Task<TResult> GetAsync<TResult>(Expression<Func<TEntity, bool>> spec,
            Expression<Func<TEntity, TResult>> selector);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

    }
}
