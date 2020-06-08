using Ejercicio2.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Repository.Interfaces
{
    public interface IUsersRepository
    {
        Task<int> AddAsync(User user);
        Task<int> UpdateAsync(User user);
        Task<int> UpdatePasswordAsync(int id, string password);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
    }
}
