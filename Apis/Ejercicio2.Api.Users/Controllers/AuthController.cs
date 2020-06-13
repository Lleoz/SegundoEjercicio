using Ejercicio2.Api.Domain.Dto;
using Ejercicio2.Api.Domain.Interfaces;
using Ejercicio2.Api.Transversal.AuthJwt.Tokens;
using Ejercicio2.Api.Transversal.HttpApi;
using Ejercicio2.Api.Users.Helpers;
using Ejercicio2.Api.Users.Requests;
using Ejercicio2.Api.Users.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ejercicio2.Api.Users.Controllers
{
    /// <summary>
    /// Controller api de Usuarios
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsers _usersDm;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Contructor del controller
        /// </summary>
        public AuthController(ILogger<UsersController> logger, 
                              IUsers usersDm,
                              IOptions<AppSettings> optionsAppSettings)
        {
            this._logger = logger;
            this._usersDm = usersDm;
            this._appSettings = optionsAppSettings.Value;
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
                    /* Agregar token con JWT */
                    var token = Jwt.GenerateToken(
                        secret: this._appSettings.Secret,
                        issuer: this._appSettings.Issuer,
                        audience: this._appSettings.Audience,
                        expirationInMinutes: int.Parse(this._appSettings.TokenExpirationInMinutes),
                        claims: new List<Claim>
                        {
                            new Claim("UserId", user.Id.ToString()),
                            new Claim("Email", user.Email)
                        });
                    var expiresInTicks = Jwt.GetExpiresTokenInTicks(int.Parse(this._appSettings.TokenExpirationInMinutes));

                    return Ok(new ApiResponse<LoginResponse>
                    {
                        Result = new LoginResponse
                        {
                            Token = token,
                            ExpiresToken = expiresInTicks
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
