using Ejercicio2.Api.Context.Sqlite;
using Ejercicio2.Api.Repository.Interfaces;
using Ejercicio2.Api.Repository.Sqlite;
using Ejercicio2.Api.UnitOfWork.Interfaces;

namespace Ejercicio2.Api.UnitOfWork.Sqlite
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly SqliteContext _context;
        private IUsersRepository _users;

        public UnitOfWorkRepository(SqliteContext context)
        {
            this._context = context;
        }

        public IUsersRepository Users => this._users ?? (this._users = new UsersRepository(this._context));
    }
}
