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
                _logger.LogError(ex, $"Ошибка получения роли с id  {ruleId}");
                throw;
            }
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

        private GetRuleDTO MapToGetRuleDTO(RuleTable rule)
        {
            return new GetRuleDTO
            {
                Id = Guid.NewGuid(),
                name = rule.name,
                description = rule.description,
                logic = rule.logic,
                status = rule.status,
                created_at = rule.created_at,
            };
        }
    }
}