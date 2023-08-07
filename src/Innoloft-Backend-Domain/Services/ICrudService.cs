using Innoloft_Backend_Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Innoloft_Backend_Domain.Services
{
    public interface ICrudService<TEntity> : ICrudService<TEntity, string>
    //where TEntity : class, IHasKey<string>
    {
    }
    public interface ICrudService<TEntity, TKey>
        //where TEntity : class, IHasKey<TKey>
        where TKey : IEquatable<TKey>
    {

        Task<GResult<TEntity>> CreateAsync(TEntity entity);

        Task<GResult<TEntity>> UpdateAsync(TEntity entity);

        Task<GResult<TEntity>> DeleteAsync(TEntity entity);

        Task<GResult<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> spec);

        Task<GResult<TResult>> ReadAsync<TResult>(Expression<Func<TEntity, bool>> spec,
            Expression<Func<TEntity, TResult>> selector);

        Task<GResult<PagedList<TResult>>> ReadListAsync<TResult>(DataSourceRequest pageRequest, Expression<Func<TEntity, bool>> spec,
            Expression<Func<TEntity, TResult>> selector);
    }
}
