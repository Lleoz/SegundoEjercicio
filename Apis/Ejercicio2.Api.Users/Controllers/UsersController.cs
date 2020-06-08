using System;
using System.Threading.Tasks;
using Ejercicio2.Api.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ejercicio2.Api.Users.Controllers
{
    /// <summary>
    /// Controller api de Usuarios
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsers _usersDm;

        /// <summary>
        /// Contructor del controller
        /// </summary>
        public UsersController(ILogger<UsersController> logger, IUsers usersDm) 
        {
            _logger = logger;
            _usersDm = usersDm;
        }

        /// <summary>
        /// Permite obtener todos los usuarios
        /// </summary>
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll() 
        {
            try
            {
                var users = await _usersDm.GetAllAsync();

                return Ok(users);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error users/getall");
                return StatusCode(500, "Error");
            }
        }
    }
}
