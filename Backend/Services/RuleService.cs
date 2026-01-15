using Backend.Interfaces;
using Backend.Middleware_Components.DTO;
using Backend.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechNest.Backend.Data;

namespace Backend.Services
{
    public class RuleService : BaseService, IRuleService
    {
        public RuleService(DataContext context, ILogger<RuleService> logger): base(context, logger) { }

        public async Task RuleFillUp(TimedRuleDTO rule)
        {
            try
            {
                var ruleEntity = new RuleTable
                {
                    Id = Guid.NewGuid(),
                    name = rule.name,
                    description = rule.description,
                    logic = rule.logic,
                    status = "Active",
                    created_at = DateTime.UtcNow,
                    created_by = rule.created_by,
                    
                };

                _context.rule_tables.Add(ruleEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка добавление роли");
                throw;
            }
        }

        public async Task<GetRuleDTO?> GetRuleFromDB(Guid ruleId)
        {
            try
            {
                var rule = await _context.rule_tables.FindAsync(ruleId);
                return rule != null ? MapToGetRuleDTO(rule) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка получения роли с id {ruleId}");
                throw;
            }
        }

        public async Task<GetRuleDTO?> GetRuleFromDB(Guid ruleId, string authHeader)
        {
            if (string.IsNullOrEmpty(authHeader))
                throw new UnauthorizedAccessException("Требуется авторизация");

            return await GetRuleFromDB(ruleId);
        }


        public async Task<List<GetRuleDTO>> GetAllRulesFromDB()
        {
            try
            {
                var rules = await _context.rule_tables
                    .OrderByDescending(r => r.created_at)
                    .ToListAsync();

                return rules.Select(MapToGetRuleDTO).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения всех ролей");
                throw;
            }
        }

        public async Task<List<GetRuleDTO>> GetRulesFromDB(string authHeader)
        {
            if (string.IsNullOrEmpty(authHeader))
                throw new UnauthorizedAccessException("Требуется авторизация");

            return await GetAllRulesFromDB();
        }

        public async Task<List<GetRuleDTO>> GetTimedRules(string authHeader)
        {
            if (string.IsNullOrEmpty(authHeader))
                throw new UnauthorizedAccessException("Требуется авторизация");

            try
            {
                var rules = await _context.rule_tables
                    .Where(r => r.status == "Active") 
                    .OrderByDescending(r => r.created_at)
                    .ToListAsync();

                return rules.Select(MapToGetRuleDTO).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения временных правил");
                throw;
            }
        }

        public async Task CreateRuleTimed(AddRuleDTO dtoObj, string authHeader)
        {
            if (string.IsNullOrEmpty(authHeader))
                throw new UnauthorizedAccessException("Требуется авторизация");

            try
            {
                var ruleEntity = new RuleTable
                {
                    Id = Guid.NewGuid(),
                    name = dtoObj.name,
                    description = dtoObj.description,
                    logic = dtoObj.logic,
                    status = "Pending", 
                    created_at = DateTime.UtcNow,
                    created_by = await GetUserIdFromAuth(authHeader), 
                };

                _context.rule_tables.Add(ruleEntity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания временного правила");
                throw;
            }
        }

        public async Task AcceptRule(string ruleName, Guid userId, string authHeader)
        {
            if (string.IsNullOrEmpty(authHeader))
                throw new UnauthorizedAccessException("Требуется авторизация");

            try
            {
                var rule = await _context.rule_tables
                    .FirstOrDefaultAsync(r => r.name == ruleName && r.status == "Active");

                if (rule == null)
                    throw new Exception($"Правило '{ruleName}' не найдено");

                _logger.LogInformation($"Пользователь {userId} принял правило {ruleName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка принятия правила {ruleName} пользователем {userId}");
                throw;
            }
        }

        private GetRuleDTO MapToGetRuleDTO(RuleTable rule)
        {
            return new GetRuleDTO
            {
                Id = rule.Id, 
                name = rule.name,
                description = rule.description,
                logic = rule.logic,
                status = rule.status,
                created_at = rule.created_at,
            };
        }

        private async Task<Guid> GetUserIdFromAuth(string authHeader)
        {
            // Пока что заглушка
            return Guid.NewGuid();
        }
    }
}