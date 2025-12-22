using Backend.Interfaces;
using Backend.Middleware_Components.DTO;
using Backend.Middleware_Components.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceUI.Controllers
{


    [Route("api/User/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Asymmetric")]
    public class UsersController : ControllerBase
    {
        //private readonly IJwtService _jwt;
        private readonly ILogger _logger;
        private readonly IBackendService _serviceBackend;

        public UsersController(/*IJwtService jwt,*/ IBackendService serviceBackend)
        {
            _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("user-controller-logger");
            //_jwt = jwt;
            _serviceBackend = serviceBackend;
        }

        //ТОЛЬКО Администратор 
        [HttpPost("Add/{id}")]
        public async Task<IActionResult> AddUser([FromBody] UserAddDTO dtoObj, string email)
        {
            try
            {
                //await _serviceBackend.AddNewUser(dtoObj, email, Request.Headers["Authorization"]);
                return Ok("user_added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //ТОЛЬКО Администратор 
        [HttpPatch("Change/{id}")]
        public async Task<IActionResult> ChangeUser([FromBody] UserChangeDTO dtoObj, Guid id)
        {
            try
            {
                //await _serviceBackend.ChangeUser(dtoObj, id, Request.Headers["Authorization"]);
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
               // await _serviceBackend.DeleteUser(id, Request.Headers["Authorization"]);
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
                //var user = await _serviceBackend.GetUser(id, Request.Headers["Authorization"]);
                return Ok("user");
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
                //var users = await _serviceBackend.GetAllUsers(from, count, Request.Headers["Authorization"]);
                return Ok("users");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}