using Ejercicio2.Api.Context.Sqlite;
using Ejercicio2.Api.Entities;
using Ejercicio2.Api.Repository.Interfaces;
using Ejercicio2.Api.Transversal.Common.Repositories;

namespace Ejercicio2.Api.Repository.Sqlite
{
    public class UsersRepository : Repository<int, User>, IUsersRepository
    {
        private readonly SqliteContext _context;

        public UsersRepository(SqliteContext context) : base(context)
        {
            this._context = context;
        }
    }
}
