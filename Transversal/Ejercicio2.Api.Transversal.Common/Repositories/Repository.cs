using Ejercicio2.Api.Transversal.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Transversal.Common.Repositories
{
    public abstract class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : Entity<TKey>
    {
        private DbContext _context { get; set; }
        private DbSet<TEntity> _dbSet => this._context.Set<TEntity>();

        public Repository(DbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this._dbSet.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetManyByAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await this._dbSet.Where(expression).ToListAsync();
        }

        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await this._dbSet.Where(expression).FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> Queryable()
        {
            return this._dbSet.AsQueryable();
        }

        public IQueryable<TEntity> QueryableBy(Expression<Func<TEntity, bool>> expression)
        {
            return this._dbSet.Where(expression).AsQueryable();
        }

        public void Add(TEntity entity)
        {
            this._dbSet.Add(entity);
            this._context.Entry(entity).State = EntityState.Added;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this._dbSet.AddRange(entities);
            this._context.Entry(entities).State = EntityState.Added;
        }

        public void Update(TEntity entity)
        {
            this._dbSet.Update(entity);
            this._context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            this._dbSet.UpdateRange(entities);
            this._context.Entry(entities).State = EntityState.Modified;
        }

        public void Delete(TKey key)
        {
            var entity = this._dbSet.Find(key);
            this._dbSet.Remove(entity);
            this._context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(TEntity entity)
        {
            this._dbSet.Remove(entity);
            this._context.Entry(entity).State = EntityState.Deleted;
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            this._dbSet.RemoveRange(entities);
            this._context.Entry(entities).State = EntityState.Deleted;
        }

        public async Task DeleteByAsync(Expression<Func<TEntity, bool>> expression)
        {
            var entities = await this.GetManyByAsync(expression);
            this._dbSet.RemoveRange(entities);
            this._context.Entry(entities).State = EntityState.Deleted;
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Repository()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}
