using Ejercicio2.Api.Entities;
using Ejercicio2.Api.Transversal.Common.Types;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ejercicio2.Api.Context.MsSql
{
    public class MsSqlContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MsSqlContext(DbContextOptions<MsSqlContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definimos la configuración de las propiedades de la entidad de User
            modelBuilder.Entity<User>(x =>
            {
                // Definimos el nombre y schema de la tabla en base de datos
                x.ToTable(name: "Users", schema: "dbo");

                // Definimos el id de la tabla de User
                x.HasKey(y => y.Id).HasName("PK_Users");

                // Definimos que el Email será único
                x.HasIndex(y => y.Email).IsUnique();

                // Definimos que el teléfono será único
                x.HasIndex(y => y.PhoneNumber).IsUnique();

                // Configuración de las propiedades
                x.Property(y => y.Id)
                    .HasColumnName("Id")
                    .HasColumnType("int")
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                x.Property(y => y.Name)
                    .HasColumnName("Name")
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100)
                    .IsRequired();
                x.Property(y => y.LastName)
                    .HasColumnName("LastName")
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100)
                    .IsRequired();
                x.Property(y => y.SecondLastName)
                    .HasColumnName("SecondLastName")
                    .HasColumnType("varchar(100)")
                    .HasMaxLength(100);
                x.Property(y => y.Email)
                    .HasColumnName("Email")
                    .HasColumnType("varchar(200)")
                    .HasMaxLength(200)
                    .IsRequired();
                x.Property(y => y.BirthDate)
                    .HasColumnName("BirthDate")
                    .HasColumnType("date")
                    .IsRequired();
                x.Property(y => y.PhoneNumber)
                    .HasColumnName("PhoneNumber")
                    .HasColumnType("varchar(20)")
                    .HasMaxLength(20)
                    .IsRequired();
                x.Property(y => y.Genre)
                    .HasColumnName("Genre")
                    .HasColumnType("tinyint")
                    .IsRequired();
                x.Property(y => y.Password)
                    .HasColumnName("Password")
                    .HasColumnType("nvarchar(max)")
                    .IsRequired();
            });

            // Agregamos datos iniciales
            //this.SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User 
                    { 
                        Id = 1,
                        Name = "admin",
                        LastName = " ",
                        SecondLastName = null,
                        Email = "admin@admin.com",
                        BirthDate = DateTime.Now,
                        PhoneNumber = "0123456789",
                        Genre = GenreType.OTHER,
                        Password = ""
                    }
                );
        }
    }
}
