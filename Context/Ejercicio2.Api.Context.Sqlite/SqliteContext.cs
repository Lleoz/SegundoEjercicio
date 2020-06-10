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
                // Definimos el nombre de la tabla en base de datos
                x.ToTable(name: "Users");

                // Definimos el id de la tabla de User
                x.HasKey(y => y.Id);

                // Definimos que el Email será único
                x.HasIndex(y => y.Email).IsUnique();

                // Definimos que el teléfono será único
                x.HasIndex(y => y.PhoneNumber).IsUnique();

                // Configuración de las propiedades
                x.Property(y => y.Id)
                    .HasColumnName("Id")
                    .HasColumnType("integer")
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                x.Property(y => y.Name)
                    .HasColumnName("Name")
                    .HasColumnType("text")
                    .HasMaxLength(100)
                    .IsRequired();
                x.Property(y => y.LastName)
                    .HasColumnName("LastName")
                    .HasColumnType("text")
                    .HasMaxLength(100)
                    .IsRequired();
                x.Property(y => y.SecondLastName)
                    .HasColumnName("SecondLastName")
                    .HasColumnType("text")
                    .HasMaxLength(100);
                x.Property(y => y.Email)
                    .HasColumnName("Email")
                    .HasColumnType("text")
                    .HasMaxLength(200)
                    .IsRequired();
                x.Property(y => y.BirthDate)
                    .HasColumnName("BirthDate")
                    .HasColumnType("numeric")
                    .IsRequired();
                x.Property(y => y.PhoneNumber)
                    .HasColumnName("PhoneNumber")
                    .HasColumnType("text")
                    .HasMaxLength(20)
                    .IsRequired();
                x.Property(y => y.Genre)
                    .HasColumnName("Genre")
                    .HasColumnType("integer")
                    .IsRequired();
                x.Property(y => y.Password)
                    .HasColumnName("Password")
                    .HasColumnType("text")
                    .IsRequired();
            });
        }
    }
}
