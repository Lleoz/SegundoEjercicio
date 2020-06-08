using Ejercicio2.Api.Repository.Interfaces;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Repository.MsSql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersDbContext context;

        public UnitOfWork(UsersDbContext context)
        {
            this.context = context;
        }

        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
