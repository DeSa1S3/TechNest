

using Backend.Middleware_Components.DTO;
using System.Security.Claims;

namespace Backend.Middleware_Components.Interfaces
{
    public interface IJwtTokensService
    {
        public string GenerateAccessToken(IEnumerable<Claim> claims, int id);

        public string GenerateRefreshToken(int id);

        public Task<bool> IsRefreshValid(int id, string token);

        public Task<bool> IsAccessValid(string? token);

        public Task<int> GetTokenUserId(string token);

        public Task<bool> RoleValid(string token, string role);

        public Task SignOut(string token);

        public Task<UserLoginDTO?> RefreshSession(string accessToken, string refreshToken);
    }
}
