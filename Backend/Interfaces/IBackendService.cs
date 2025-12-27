using Backend.Middleware_Components.DTO;
using Middleware_Components.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface IBackendService
    {
        Task<string?> ClientSignOut(string bearer_key);
        Task<AuthTokenInfo?> AuthPart(string username, string password);
        Task<AuthTokenInfo?> RefreshClientSession(string refreshTokenDTO);
        Task AddNewUser(UserAddDTO dtoObj, string email, string token);
        Task ChangeUser(UserChangeDTO dtoObj, Guid id, string token);
        Task DeleteUser(Guid idUser, string token);
        Task<List<UserGetDTO>> GetAllUsers(int from, int count, string token);
        Task<UserGetDTO?> GetUser(Guid idUser, string token);
        Task<List<List<TimedRuleDTO>?>> GetTimedRules(string token);
        Task CreateRuleTimed(AddRuleDTO addRuleDTO, string token);
        Task AcceptRule(string ruleName, Guid idUser, string token);
        Task<GetRuleDTO?> GetRuleFromDB(Guid ruleId, string token);
        Task<List<GetRuleDTO>?> GetRulesFromDB(string token);
        Task<MeDTO> GetInfoMe(string token);
    }
}