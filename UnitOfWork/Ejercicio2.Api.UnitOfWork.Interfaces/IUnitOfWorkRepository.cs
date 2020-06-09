using Ejercicio2.Api.Repository.Interfaces;

namespace Ejercicio2.Api.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        IUsersRepository UsersRepository { get; }
    }
}
