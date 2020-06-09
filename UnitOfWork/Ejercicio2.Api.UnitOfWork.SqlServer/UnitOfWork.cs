using Ejercicio2.Api.Context.MsSql;
using Ejercicio2.Api.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Ejercicio2.Api.UnitOfWork.MsSql
{
    public class UnitOfWork : IUnitOfWork
    {
        private MsSqlContext _context { get; set; }
        private IDbContextTransaction _transaction { get; set; }
        private IUnitOfWorkRepository _repositories { get; set; }

        public IUnitOfWorkRepository Repositories => this._repositories;

        public UnitOfWork(MsSqlContext context)
        {
            this._context = context;
            this._transaction = this._context.Database.BeginTransaction();
            this._repositories = this._repositories = new UnitOfWorkRepository(this._context);
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                var savedElements = await this._context.SaveChangesAsync();
                this._transaction.Commit();

                if (savedElements > -1)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                this._transaction.Rollback();
                throw ex;
            }
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

                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                    }

                    if (_context != null)
                    {
                        _context.Database.CloseConnection();
                        _context.Dispose();
                    }

                    this._repositories = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork()
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
