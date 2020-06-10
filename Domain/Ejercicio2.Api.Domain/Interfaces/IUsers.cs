using Ejercicio2.Api.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Domain.Interfaces
{
    public interface IUsers
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByAsync(string email, string password);
        Task<UserDto> GetByAsync(int id);
        Task<UserDto> GetByAsync(string email);
        Task<UserDataDto> AddAsync(UserDto user);
        Task<bool> UpdateAsync(UserDto user);
        Task<UserDataDto> UpdatePasswordAsync(string email);
        Task<bool> DeleteAsync(int id);
    }
}
