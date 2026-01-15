using Backend.Middleware_Components.DTO;

namespace Backend.Interfaces
{
    public interface IRuleService
    {
        Task RuleFillUp(TimedRuleDTO rule);
        Task<GetRuleDTO?> GetRuleFromDB(Guid ruleId);
        Task<List<GetRuleDTO>> GetAllRulesFromDB();
        Task CreateRuleTimed(AddRuleDTO dtoObj, string authHeader);
        Task AcceptRule(string ruleName, Guid userId, string authHeader);
        Task<List<GetRuleDTO>> GetTimedRules(string authHeader);

        // Метод для получения правил с авторизацией
        Task<List<GetRuleDTO>> GetRulesFromDB(string authHeader);

        // Метод для получения одного правила с авторизацией
        Task<GetRuleDTO?> GetRuleFromDB(Guid ruleId, string authHeader);
    }
}
