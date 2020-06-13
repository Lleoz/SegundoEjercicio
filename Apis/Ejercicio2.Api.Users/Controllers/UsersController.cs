using Ejercicio2.Api.Domain.Dto;
using Ejercicio2.Api.Domain.Interfaces;
using Ejercicio2.Api.Transversal.AuthJwt.Controllers;
using Ejercicio2.Api.Transversal.HttpApi;
using Ejercicio2.Api.Users.Helpers;
using Ejercicio2.Api.Users.Requests;
using Ejercicio2.Api.Users.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Users.Controllers
{
    /// <summary>
    /// Controller api de Usuarios
    /// </summary>
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UsersController : AuthorizationController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsers _usersDm;
        private readonly ISecurityDm _securityDm;

        /// <summary>
        /// Contructor del controller
        /// </summary>
        public UsersController(ILogger<UsersController> logger, 
                               IUsers usersDm,
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
        public async Task<ActionResult<ApiResponse<IEnumerable<UserResponse>>>> GetAll()
        {
            try
            {
                // Ejemplo para obtener las propiedades en la sesión del Token con JWT
                var userId = this.AppSession.GetPropertyValue<int>("UserId");
                var email = this.AppSession.GetPropertyValue<string>("Email");

                var users = await _usersDm.GetAllAsync();

                if (users != null && users.Any())
                {
                    return Ok(new ApiResponse<IEnumerable<UserResponse>>
                    {
                        Result = users
                            .Select(x => new UserResponse 
                            {
                                Id = x.Id,
                                Name = x.Name,
                                LastName = x.LastName,
                                SecondLastName = x.SecondLastName,
                                Email = x.Email,
                                BirthDate = x.BirthDate,
                                PhoneNumber = x.PhoneNumber,
                                Genre = x.Genre,
                                FullName = x.FullName
                            })
                            .AsEnumerable(),
                        Error = $"{userId.ToString()} - {email}",
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
        public async Task<ActionResult<ApiResponse<UserResponse>>> Get([FromRoute] string email)
        {
            try
            {
                var user = await _usersDm.GetByAsync(email);

                if (user != null)
                {
                    return Ok(new ApiResponse<UserResponse>
                    {
                        Result = new UserResponse 
                        {
                            Id = user.Id,
                            Name = user.Name,
                            LastName = user.LastName,
                            SecondLastName = user.SecondLastName,
                            Email = user.Email,
                            BirthDate = user.BirthDate,
                            PhoneNumber = user.PhoneNumber,
                            Genre = user.Genre,
                            FullName = user.FullName
                        },
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
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<UserCreateResponse>>> Post([FromBody] ApiRequest<UserRequest> request)
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
                    try
                    {
                        /* Envía por correo el password */
                        _securityDm.SendPasswordByEmail(userDto, userData.Password);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                    
                    return Ok(new ApiResponse<UserCreateResponse>
                    {
                        Result = new UserCreateResponse 
                        {
                            Id = userData.Id,
                            Password = userData.Password
                        },
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
                    try
                    {
                        /* Envía por correo el password */
                        this._securityDm.SendPasswordByEmail(
                            name: userData.FullName,
                            email: userData.Email,
                            password: userData.Password);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }

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
    }
}
