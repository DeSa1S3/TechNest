using Backend.Interfaces;
using Backend.Middleware_Components.DTO;
using Backend.Middleware_Components.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/Rules/")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly IJwtTokensService _jwtTokensService;
        private readonly ILogger<RulesController> _logger;
        private readonly IBackendService _backendService;

        public RulesController( IJwtTokensService jwtTokensService,IBackendService backendService,ILogger<RulesController> logger)
        {
            _jwtTokensService = jwtTokensService;
            _backendService = backendService;
            _logger = logger;
        }

        [HttpPost("TimedRules/Add")]
        public async Task<IActionResult> AddTimedRule(AddRuleDTO dtoObj)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                await _backendService.CreateRuleTimed(dtoObj, authHeader);
                return Ok("правило_добавлено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении временного правила");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("TimedRules/Accept/{userId}/{ruleName}")]
        public async Task<IActionResult> AcceptTimedRule(Guid userId, string ruleName)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                await _backendService.AcceptRule(ruleName, userId, authHeader);
                return Ok("правило_принято");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при принятии временного правила");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("TimedRules/All")]
        public async Task<IActionResult> GetTimedRules()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var rules = await _backendService.GetTimedRules(authHeader);

                if (rules != null)
                    return Ok(rules);

                return NotFound("Временные правила не найдены");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении временных правил");
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("Add")]
        //public async Task<IActionResult> AddRule(AddRuleDTO dtoObj)
        //{
        //    try
        //    {
        //        var authHeader = Request.Headers["Authorization"].ToString();
        //        if (string.IsNullOrEmpty(authHeader))
        //        {
        //            return Unauthorized("Заголовок Authorization отсутствует");
        //        }

        //        await _backendService.CreateRuleTimed(dtoObj, authHeader);
        //        return Ok("правило_добавлено");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Ошибка при добавлении правила");
        //        return BadRequest(ex.Message);
        //    }
        //}

        // Изменение правила (заглушка)
        [HttpPatch("Change")]
        public async Task<IActionResult> ChangeRule()
        {
            return BadRequest("Метод еще не реализован");
        }

        // Удаление правила (заглушка)
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRule(Guid id)
        {
            return BadRequest("Метод еще не реализован");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRule(Guid id)
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var rule = await _backendService.GetRuleFromDB(id, authHeader);

                if (rule != null)
                    return Ok(rule);

                return NotFound($"Правило с id {id} не найдено");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении правила с id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetRules()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrEmpty(authHeader))
                {
                    return Unauthorized("Заголовок Authorization отсутствует");
                }

                var rules = await _backendService.GetRulesFromDB(authHeader);

                if (rules != null && rules.Count > 0)
                    return Ok(rules);

                return NotFound("Правила в базе данных не найдены");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех правил");
                return BadRequest(ex.Message);
            }
        }
    }
}