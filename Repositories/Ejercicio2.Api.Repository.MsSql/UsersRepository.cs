using Ejercicio2.Api.Context.MsSql;
using Ejercicio2.Api.Entities;
using Ejercicio2.Api.Repository.Interfaces;
using Ejercicio2.Api.Transversal.Common.Repositories;

namespace Ejercicio2.Api.Repository.MsSql
{
    public class UsersRepository : Repository<int, User>, IUsersRepository
    {
        private readonly MsSqlContext _context;

        public UsersRepository(MsSqlContext context) : base(context)
        {
            this._context = context;
        }
    }
}
