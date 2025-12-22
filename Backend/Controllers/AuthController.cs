using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Data;
using TechNest.Backend.Data;
using Backend.Middleware_Components.DTO;
using Backend.Tables;
using Backend.Middleware_Components.Interfaces;
using System.ComponentModel.DataAnnotations;
using Middleware_Components.DTO;
using System.Text.Json;

namespace Backend.Controllers
{
    [Route("api/Auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtTokensService _jwt;

        public AuthController(IConfiguration configuration, IJwtTokensService jwt)
        {
            _configuration = configuration;
            _jwt = jwt;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register_DTO)
        {
            using (DataContext DB = new DataContext())
            {
                UsersTable userTable = new UsersTable()
                {
                    firstName = register_DTO.firstName,
                    lastName = register_DTO.lastName,
                    RegistrationDate = register_DTO.RegistrationDate,
                    Email = register_DTO.Email,
                    Password = register_DTO.Password,
                    Roles = new string[] { "User" }
                };
                DB.user_table.Add(userTable);
                await DB.SaveChangesAsync();
                return Ok("Успех!+");

            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AuthDTO login_DTO)
        {
            using (DataContext DB = new DataContext())
            {
                foreach (var user in DB.user_table)
                {
                    if (user.Email == login_DTO.Email && user.Password == login_DTO.Password)
                    {

                        var serializer_roles = JsonSerializer.Serialize(user.Roles);

                        var claims = new[]
                        {
                            new Claim("Id", user.Id.ToString()),
                            new Claim("Email", user.Email),
                            new Claim("Role", serializer_roles),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };


                        var accessTokenGenerated = _jwt.GenerateAccessToken(claims, user.Id);

                        var refreshTokenGenerated = _jwt.GenerateRefreshToken(user.Id);

                        return Ok(new UserLoginDTO { accessToken = accessTokenGenerated, refreshToken = refreshTokenGenerated });
                    }
                }

                return BadRequest("Не правильная почта или пароль");
            }
        }

        [HttpGet]
        [Route("Validate")]
        public async Task<IActionResult> ValidateToken(string? token)
        {
            if (token != null)
            {
                if (token.Contains("Bearer"))
                    return BadRequest("accessToken in this method must not contain word [Bearer]");
            }

            var validation = await _jwt.IsAccessValid(token);


            if (!validation)
            {
                return Unauthorized();
            }
            else if (validation)
            {
                return Ok("valid");
            }

            return BadRequest();
        }


        [Authorize(AuthenticationSchemes = "Asymmetric")]
        [HttpPut]
        [Route("SignOut")]
        public async Task<IActionResult> UserSignOut()
        {
            try
            {
                var tokenFromRequest = Request.Headers["Authorization"].ToString();

                await _jwt.SignOut(tokenFromRequest.Substring("Bearer ".Length));

                return Ok("Вы вышли");
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }

        [Authorize(AuthenticationSchemes = "Asymmetric")]
        [HttpPost]
        [Route("Refresh")]
        public async Task<IActionResult> UserRefreshTokens(AuthRefreshToken dtoObj)
        {
            try
            {
                var tokenFromRequest = Request.Headers["Authorization"].ToString();

                var refreshedSession = await _jwt.RefreshSession(tokenFromRequest.Substring("Bearer ".Length), dtoObj.refreshToken);

                return Ok(refreshedSession);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
    }
}