using System;
using System.Threading.Tasks;

namespace Ejercicio2.Api.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUnitOfWorkRepository Repositories { get; }

        Task<bool> SaveAsync();
    }
}
