using Backend.Middleware_Components.DTO;

namespace Backend.Interfaces
{
    public interface IRuleService
    {
        Task RuleFillUp(TimedRuleDTO rule);
        Task<GetRuleDTO?> GetRuleFromDB(Guid ruleId);
        Task<List<GetRuleDTO>> GetAllRulesFromDB();
    }
}
