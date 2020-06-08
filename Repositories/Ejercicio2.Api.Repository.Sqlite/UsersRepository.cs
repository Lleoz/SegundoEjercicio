using Ejercicio2.Api.Entities;
using Ejercicio2.Api.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Repository.Sqlite
{
    public class UsersRepository : IUsersRepository
    {
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
            throw new NotImplementedException();
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
