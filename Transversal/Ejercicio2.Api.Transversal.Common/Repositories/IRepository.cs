using Ejercicio2.Api.Transversal.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Transversal.Common.Repositories
{
    public interface IRepository<TKey, TEntity> : IDisposable where TEntity : Entity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetManyByAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> QueryableBy(Expression<Func<TEntity, bool>> expression);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Delete(TKey key);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task DeleteByAsync(Expression<Func<TEntity, bool>> expression);
    }
}
