using AutoMapper;
using Ejercicio2.Api.Context.MsSql;
using Ejercicio2.Api.Context.Sqlite;
using Ejercicio2.Api.Domain;
using Ejercicio2.Api.Domain.Interfaces;
using Ejercicio2.Api.Repository.Interfaces;
using Ejercicio2.Api.Repository.MsSql;
using Ejercicio2.Api.UnitOfWork.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Ejercicio2.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCustomDomain()
                    .AddCustomDbContextSqlite(Configuration)
                    .AddCustomDbContextMsSql(Configuration)
                    .AddSwagger();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ejemplo2api V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomDomain(this IServiceCollection services)
        {
            services.AddScoped<IUsers, UsersDm>();
            return services;
        }

        /// <summary>
        /// Configura e inyecta el contexto por DI en el contexto de la aplicacion/api
        /// </summary>
        public static IServiceCollection AddCustomDbContextMsSql(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<MsSqlContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SqlServerContext")));

            services.AddScoped<IUnitOfWork, UnitOfWork.MsSql.UnitOfWork>();
            services.AddScoped<IUsersRepository, Repository.MsSql.UsersRepository>();

            return services;
        }

        public static IServiceCollection AddCustomDbContextSqlite(this IServiceCollection services, IConfiguration Configuration)
        {
            var connection = new SqliteConnection(Configuration.GetConnectionString("SqliteContext"));
            connection.Open();
            services.AddDbContext<SqliteContext>(options =>
                    options.UseSqlite(connection));

            services.AddScoped<IUnitOfWork, UnitOfWork.Sqlite.UnitOfWork>();
            services.AddScoped<IUsersRepository, Repository.Sqlite.UsersRepository>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ejercicio2",
                    Description = "Ejercicio 2",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "ejercicio2api",
                        Email = string.Empty,
                        Url = new Uri("https://example.com/contact"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
