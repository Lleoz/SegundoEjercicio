using Ejercicio2.Api.Domain.Dto;
using Ejercicio2.Api.Domain.Interfaces;
using Ejercicio2.Api.Entities;
using Ejercicio2.Api.Transversal.HttpApi;
using Ejercicio2.Api.Users.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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
        public async Task<ActionResult<ApiResponse<IEnumerable<User>>>> GetAll() 
        {
            try
            {
                var users = await _usersDm.GetAllAsync();

                if (users != null && users.Any())
                {
                    return Ok(new ApiResponse<IEnumerable<UserDto>>
                    {
                        Result = users,
                        Error = null,
                        Status = HttpStatusCode.OK
                    });
                }
                else
                {
                    return NotFound(new ApiResponse<UserDto>
                    {
                        Result = null,
                        Error = "Usuarios no encontrados",
                        Status = HttpStatusCode.NotFound
                    });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error users/getall");
                return BadRequest(new ApiResponse<IEnumerable<User>>
                {
                    Result = null,
                    Error = e.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        /// <summary>
        /// Recibe un objeto
        /// </summary>
        [HttpPost]
        [Route("recibeobjeto")]
        public async Task<ActionResult> RecibeObjeto([FromBody]ApiRequest<LoginRequest> request)
        {
            try
            {
                return Ok(new ApiResponse
                {
                    Error = null,
                    Status = HttpStatusCode.OK
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error users/getall");
                return BadRequest(new ApiResponse
                {
                    Error = e.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }
    }
}
