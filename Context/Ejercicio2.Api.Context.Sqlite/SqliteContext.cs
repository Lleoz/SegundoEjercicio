using Ejercicio2.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ejercicio2.Api.Context.Sqlite
{
    public class SqliteContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SqliteContext(DbContextOptions<SqliteContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definimos la configuración de las propiedades de la entidad de User
            modelBuilder.Entity<User>(x =>
            {
                // Definimos el id de la tabla de User
                x.HasKey(y => y.Id);
                x.Property(y => y.Id)
                    .HasColumnName("Id")
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                x.Property(y => y.FullName).HasColumnName("FullName").IsRequired();
                x.Property(y => y.Email).HasColumnName("Email").IsRequired();
                x.Property(y => y.BirthDate).HasColumnName("BirthDate").IsRequired();
                x.Property(y => y.Genre).HasColumnName("Genre").IsRequired();
                x.Property(y => y.Password).HasColumnName("Password").IsRequired();
            });
        }
    }
}
