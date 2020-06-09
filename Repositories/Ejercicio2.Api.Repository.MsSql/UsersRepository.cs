using Ejercicio2.Api.Context.MsSql;
using Ejercicio2.Api.Entities;
using Ejercicio2.Api.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Repository.MsSql
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MsSqlContext _context;

        public UsersRepository(MsSqlContext context)
        {
            this._context = context;
        }

        public async Task<int> AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return new List<User>()
                {
                    new User()
                    {
                        Id = 1,
                        BirthDate = new DateTime(1998,1,1),
                        Email = "user1@gmail.com",
                        FullName = "Usuario 1",
                        Genre = 1,
                        PhoneNumber = "+7777777777"
                    }
                };
            });
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdatePasswordAsync(int id, string password)
        {
            throw new NotImplementedException();
        }
    }
}
