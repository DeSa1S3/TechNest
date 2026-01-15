using Backend.Interfaces;
using Backend.Middleware_Components.DTO;
using Backend.Middleware_Components.Interfaces;
using Backend.Middleware_Components.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace ServiceUI.Controllers
{
    [Route("api/User/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Asymmetric")]
    public class UsersController : ControllerBase
    {
        private readonly JwtTokensService _jwt;
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public UsersController(JwtTokensService jwt, IUserService userService)
        {
            _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("user-controller-logger");
            _jwt = jwt;
            _userService = userService;
        }

        //ТОЛЬКО Администратор 
        [HttpPost("Add/{id}")]
        public async Task<IActionResult> AddUser([FromBody] UserAddDTO dtoObj, string email)
        {
            try
            {
                var authHeader = GetAuthHeader();
                await _userService.AddUser(dtoObj, email, authHeader);
                return Ok("user_added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //ТОЛЬКО Администратор 
        [HttpPatch("Change/{id}")]
        public async Task<IActionResult> ChangeUser(Guid id, [FromBody] UserChangeDTO dtoObj) // Изменен порядок параметров
        {
            try
            {
                var authHeader = GetAuthHeader();
                await _userService.ChangeUser(dtoObj, id, authHeader);
                return Ok("user_changed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //ТОЛЬКО Администратор 
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var authHeader = GetAuthHeader();
                await _userService.DeleteUser(id, authHeader);
                return Ok("user_deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var authHeader = GetAuthHeader();
                var user = await _userService.GetUser(id, authHeader);
                return Ok(user); // Исправлено: возвращаем объект user, а не строку "user"
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetUsers([FromQuery] int from, [FromQuery] int count)
        {
            try
            {
                var authHeader = GetAuthHeader();
                var users = await _userService.GetAllUsers(from, count, authHeader);
                return Ok(users); // Исправлено: возвращаем объект users, а не строку "users"
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GetAuthHeader()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authHeader))
            {
                throw new UnauthorizedAccessException("Заголовок Authorization отсутствует");
            }
            return authHeader;
        }
    }
}