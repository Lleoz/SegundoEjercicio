using System.Threading.Tasks;

namespace Ejercicio2.Api.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
    }
}
