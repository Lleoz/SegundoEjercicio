using Ejercicio2.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio2.Api.Repository.MsSql
{
    public class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
