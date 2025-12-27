using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Middleware_Components.DTO;

namespace Backend.Middleware_Components.Interfaces
{
    public interface IJwtTokensService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims, int id);
        string GenerateRefreshToken(int id);
        Task<bool> IsRefreshValid(int id, string token);
        Task<bool> IsAccessValid(string? token);
        Task<int> GetTokenUserId(string token);
        Task<bool> RoleValid(string token, string role);
        Task SignOut(string token);
        Task<UserLoginDTO?> RefreshSession(string accessToken, string refreshToken);
        Task<TokenValidateResultDTO> AccessTokenValidation(string token);
    }
}