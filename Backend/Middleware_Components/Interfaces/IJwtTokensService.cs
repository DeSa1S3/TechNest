using Backend.Middleware_Components.DTO;
using System.Security.Claims;

public interface IJwtTokensService
{
    string GenerateAccessToken(IEnumerable<Claim> claims, Guid id);
    string GenerateRefreshToken(Guid id);
    Task<bool> IsRefreshValid(Guid id, string token); 
    Task<bool> IsAccessValid(string? token);
    Task<Guid> GetTokenUserId(string token); 
    Task<bool> RoleValid(string token, string role);
    Task SignOut(string token);
    Task<UserLoginDTO?> RefreshSession(string accessToken, string refreshToken);
    Task<TokenValidateResultDTO> AccessTokenValidation(string token);
}