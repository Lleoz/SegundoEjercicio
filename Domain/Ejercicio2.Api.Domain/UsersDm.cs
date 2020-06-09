using Ejercicio2.Api.Domain.Dto;
using Ejercicio2.Api.Domain.Interfaces;
using Ejercicio2.Api.UnitOfWork.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Domain
{
    public class UsersDm : IUsers
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersDm(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.Repositories.UsersRepository.GetAllAsync();

            if (users == null)
            {
                return null;
            }

            return users
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    BirthDate = x.BirthDate,
                    PhoneNumber = x.PhoneNumber
                })
                .AsEnumerable();
        }
    }
}
