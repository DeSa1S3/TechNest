using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Data;
using Backend_AA.Model.DTO;
using Backend_AA.Model.DataContext;
using Backend_AA.Model.DBO;

namespace Backend_AA.Controllers
{
    [Route("api/Auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
     

        public AuthController (IConfiguration configuration)
        {
            _configuration = configuration;
        }
 
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] Register_DTO register_DTO)
        {
            using(DataContext DB = new DataContext())
            {
                UserTable userTable = new UserTable() { 
                    firstName = register_DTO.firstName,
                    lastName = register_DTO.lastName,
                    DateOfBirth = register_DTO.DateOfBirth,
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
        public async Task<IActionResult> Login([FromBody] Login_DTO login_DTO)
        {
            using(DataContext DB = new DataContext())
            {
                foreach (var user in DB.user_table)
                {
                    if(user.Email == login_DTO.Email && user.Password == login_DTO.Password)
                    {
                        return Ok("Успешный вход");
                    }
                }
                return BadRequest("Не правильная почта или пароль");
            }
        }

    }