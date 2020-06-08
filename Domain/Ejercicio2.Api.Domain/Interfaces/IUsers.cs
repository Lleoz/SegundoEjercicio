using Ejercicio2.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Domain.Interfaces
{
    public interface IUsers
    {
        Task<IEnumerable<User>> GetAllAsync();
    }
}
