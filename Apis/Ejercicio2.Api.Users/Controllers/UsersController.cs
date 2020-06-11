using Ejercicio2.Api.Domain.Dto;
using Ejercicio2.Api.Domain.Interfaces;
using Ejercicio2.Api.Transversal.Email;
using Ejercicio2.Api.Transversal.HttpApi;
using Ejercicio2.Api.Users.Requests;
using Ejercicio2.Api.Users.Responses;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ISecurityDm _securityDm;

        /// <summary>
        /// Contructor del controller
        /// </summary>
        public UsersController(ILogger<UsersController> logger, IUsers usersDm,
                               ISecurityDm securityDm)
        {
            _logger = logger;
            _usersDm = usersDm;
            _securityDm = securityDm;
        }

        /// <summary>
        /// Permite obtener todos los usuarios
        /// </summary>
        [HttpGet("getall")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserDto>>>> GetAll()
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
                    return NotFound(new ApiResponse
                    {
                        Error = "Users not found",
                        Status = HttpStatusCode.NotFound
                    });
                }
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

        /// <summary>
        /// Permite obtener los datos de un usuario
        /// </summary>
        [HttpGet("get/{email}")]
        public async Task<ActionResult<ApiResponse<UserDto>>> Get([FromRoute] string email)
        {
            try
            {
                var user = await _usersDm.GetByAsync(email);

                if (user != null)
                {
                    return Ok(new ApiResponse<UserDto>
                    {
                        Result = user,
                        Error = null,
                        Status = HttpStatusCode.OK
                    });
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Error = "Users not found",
                        Status = HttpStatusCode.NotFound
                    });
                }
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

        /// <summary>
        /// Permite agregar un nuevo usuario
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<int>>> Post([FromBody] ApiRequest<UserRequest> request)
        {
            try
            {
                UserDto userDto = new UserDto
                {
                    Name = request.Data.Name,
                    LastName = request.Data.LastName,
                    SecondLastName = request.Data.SecondLastName,
                    Email = request.Data.Email,
                    BirthDate = request.Data.BirthDate,
                    PhoneNumber = request.Data.PhoneNumber,
                    Genre = request.Data.Genre
                };
                var userData = await this._usersDm.AddAsync(userDto);

                if (userData != null && userData.Id > 0 && !String.IsNullOrWhiteSpace(userData.Password))
                {
                    /* Envía por correo el password */
                    _securityDm.SendPasswordByEmail(userDto, userData.Password);

                    return Ok(new ApiResponse<int>
                    {
                        Result = userData.Id,
                        Error = null,
                        Status = HttpStatusCode.OK
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Error = "Ha ocurrido un error al agregar al usuario",
                        Status = HttpStatusCode.BadRequest
                    });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error users/login");
                return BadRequest(new ApiResponse
                {
                    Error = e.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        /// <summary>
        /// Permite editar la información de un usuario
        /// </summary>
        [HttpPut("put/{id}")]
        public async Task<ActionResult<ApiResponse>> Put([FromRoute] int id, [FromBody] ApiRequest<UserRequest> request)
        {
            try
            {
                var isUpdated = await this._usersDm.UpdateAsync(new UserDto
                {
                    Id = id,
                    Name = request.Data.Name,
                    LastName = request.Data.LastName,
                    SecondLastName = request.Data.SecondLastName,
                    Email = request.Data.Email,
                    BirthDate = request.Data.BirthDate,
                    PhoneNumber = request.Data.PhoneNumber,
                    Genre = request.Data.Genre
                });

                if (isUpdated)
                {
                    return Ok(new ApiResponse
                    {
                        Error = null,
                        Status = HttpStatusCode.OK
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Error = "Ha ocurrido un error al agregar al usuario",
                        Status = HttpStatusCode.BadRequest
                    });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error users/login");
                return BadRequest(new ApiResponse
                {
                    Error = e.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        /// <summary>
        /// Permite editar el password del usuario y ésta es envíada por correo
        /// </summary>
        [HttpPut("changepassword/{email}")]
        public async Task<ActionResult<ApiResponse>> ChangePassword([FromRoute] string email)
        {
            try
            {
                var userData = await this._usersDm.UpdatePasswordAsync(email);

                if (userData != null && userData.Id > 0 && !String.IsNullOrWhiteSpace(userData.Password))
                {
                    /**
                     
                        Envíar por correo el password    
                    
                     */

                    return Ok(new ApiResponse
                    {
                        Error = null,
                        Status = HttpStatusCode.OK
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Error = "Ha ocurrido un error al agregar al usuario",
                        Status = HttpStatusCode.BadRequest
                    });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error users/login");
                return BadRequest(new ApiResponse
                {
                    Error = e.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        /// <summary>
        /// Permite eliminar un usuario
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResponse>> Delete([FromRoute] int id)
        {
            try
            {
                var isDeleted = await this._usersDm.DeleteAsync(id);

                if (isDeleted)
                {
                    return Ok(new ApiResponse
                    {
                        Error = null,
                        Status = HttpStatusCode.OK
                    });
                }
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Error = "Ha ocurrido un error al agregar al usuario",
                        Status = HttpStatusCode.BadRequest
                    });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error users/login");
                return BadRequest(new ApiResponse
                {
                    Error = e.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        /// <summary>
        /// Permite iniciar sesión regresando un token
        /// </summary>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> Login([FromBody] ApiRequest<LoginRequest> request)
        {
            try
            {
                var user = await this._usersDm
                    .GetByAsync(
                        email: request.Data.Email,
                        password: request.Data.Password);

                if (user != null)
                {
                    /**
                     
                    Agregar token con JWT
                     
                     */

                    return Ok(new ApiResponse<LoginResponse>
                    {
                        Result = new LoginResponse
                        {
                            Token = null,
                            ExpiresToken = 0
                        },
                        Error = null,
                        Status = HttpStatusCode.OK
                    });
                }
                else
                {
                    return NotFound(new ApiResponse
                    {
                        Error = "User not found",
                        Status = HttpStatusCode.NotFound
                    });
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error users/login");
                return BadRequest(new ApiResponse
                {
                    Error = e.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }

        /// <summary>
        /// Permite volver a regenerar el token
        /// </summary>
        [AllowAnonymous]
        [HttpPut("refreshtoken/{token}")]
        public async Task<ActionResult<ApiResponse<LoginResponse>>> RefreshToken([FromRoute] string token)
        {
            try
            {
                return Ok(new ApiResponse<LoginResponse>
                {
                    Result = new LoginResponse
                    {
                        Token = null,
                        ExpiresToken = 0
                    },
                    Error = null,
                    Status = HttpStatusCode.OK
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error users/login");
                return BadRequest(new ApiResponse
                {
                    Error = e.Message,
                    Status = HttpStatusCode.BadRequest
                });
            }
        }
    }
}
