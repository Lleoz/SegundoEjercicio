using Ejercicio2.Api.Domain.Interfaces;
using Ejercicio2.Api.Entities;
using Ejercicio2.Api.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Domain
{
    public class UsersDm : IUsers
    {
        private readonly IUsersRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsersDm(IUsersRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}
