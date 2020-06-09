using Ejercicio2.Api.Context.MsSql;
using Ejercicio2.Api.Repository.Interfaces;
using Ejercicio2.Api.Repository.MsSql;
using Ejercicio2.Api.UnitOfWork.Interfaces;

namespace Ejercicio2.Api.UnitOfWork.MsSql
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly MsSqlContext _context;
        private IUsersRepository _usersRepository;

        public UnitOfWorkRepository(MsSqlContext context)
        {
            this._context = context;
        }

        public IUsersRepository UsersRepository => this._usersRepository ?? (this._usersRepository = new UsersRepository(this._context));
    }
}
