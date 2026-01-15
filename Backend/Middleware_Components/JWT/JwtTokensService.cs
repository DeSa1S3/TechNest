using Backend.Middleware_Components.DTO;
using Backend.Middleware_Components.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Backend.Middleware_Components.JWT
{
    public class JwtTokensService : IJwtTokensService
    {
        private readonly IConfiguration _config;
        private readonly ICacheService _cache;
        private readonly ILogger<JwtTokensService> _logger;

        public JwtTokensService(ICacheService cache, IConfiguration config, ILogger<JwtTokensService> logger)
        {
            _config = config;
            _cache = cache;
            _logger = logger;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims, Guid id)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("JwtSettings")["SecretKey"] ?? "default_secret_key_32_chars_long_1234567890"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config.GetSection("JwtSettings")["Issuer"] ?? "Backend",
                audience: _config.GetSection("JwtSettings")["Audience"] ?? "Frontend",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: creds
            );

            var tokenReleased = new JwtSecurityTokenHandler().WriteToken(token);

            if (_cache.CheckExistKeysStorage(id, "accessTokens"))
                _cache.DeleteKeyFromStorage(id, "accessTokens");

            _cache.WriteKeyInStorage(id, "accessTokens", tokenReleased,
                DateTime.UtcNow.AddMinutes(double.Parse(_config.GetSection("JwtSettings")["ExpiryInMinutesAccess"] ?? "15")));

            _logger.LogInformation($"Сгенерирован токен доступа для {id}");
            return tokenReleased;
        }

        public string GenerateRefreshToken(Guid id)
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            var genToken = Convert.ToBase64String(randomBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");

            if (_cache.CheckExistKeysStorage(id, "refreshTokens"))
                _cache.DeleteKeyFromStorage(id, "refreshTokens");

            _cache.WriteKeyInStorage(id, "refreshTokens", genToken,
                DateTime.UtcNow.AddDays(double.Parse(_config.GetSection("JwtSettings")["ExpiryInDaysRefresh"] ?? "7")));

            _logger.LogInformation($"Сгенерирован токен обновления для {id}");
            return genToken;
        }

        private async Task<JwtSecurityToken?> ValidateTokenAsync(string? token)
        {
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("Token is null or empty");
                return null;
            }

            if (token.StartsWith("Bearer "))
            {
                token = token.Substring(7);
            }

            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _config.GetSection("JwtSettings")["Issuer"] ?? "Backend",
                ValidAudience = _config.GetSection("JwtSettings")["Audience"] ?? "Frontend",
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings")["SecretKey"] ?? "default_secret_key_32_chars_long_1234567890")),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var rawValidatedToken);
                return (JwtSecurityToken)rawValidatedToken;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Token validation failed");
                return null;
            }
        }

        // ИЗМЕНИТЬ НА Guid!
        public async Task<bool> IsRefreshValid(Guid id, string token)
        {
            if (_cache.CheckExistKeysStorage(id, "refreshTokens"))
            {
                var tokenFromCache = _cache.GetKeyFromStorage<string>(id, "refreshTokens");
                return tokenFromCache != null && tokenFromCache == token;
            }
            return false;
        }

        public async Task<bool> IsAccessValid(string? token)
        {
            var validation = await ValidateTokenAsync(token);
            if (validation == null) return false;

            
            var userId = GetUserIdFromToken(validation);
            if (userId.HasValue)
            {
                var tokenFromCache = _cache.GetKeyFromStorage<string>(userId.Value, "accessTokens");
                return tokenFromCache != null && tokenFromCache == token;
            }

            return false;
        }

        private Guid? GetUserIdFromToken(JwtSecurityToken token)
        {
            foreach (var claim in token.Claims)
            {
                if (claim.Type == "Id" || claim.Type == ClaimTypes.NameIdentifier)
                {
                    if (Guid.TryParse(claim.Value, out Guid userId))
                        return userId;
                }
            }
            return null;
        }

        public async Task<Guid> GetTokenUserId(string token)
        {
            var validation = await ValidateTokenAsync(token);
            if (validation == null)
                throw new Exception("Token validation failed");

            var userId = GetUserIdFromToken(validation);
            if (userId.HasValue)
                return userId.Value;

            throw new Exception("User Id not found in token");
        }

        public async Task<bool> RoleValid(string token, string role)
        {
            var validation = await ValidateTokenAsync(token);
            if (validation == null)
                throw new Exception("Token validation failed");

            foreach (var claim in validation.Claims)
            {
                if (claim.Type == "Role" || claim.Type == ClaimTypes.Role)
                {
                    try
                    {
                        var roles = JsonSerializer.Deserialize<List<string>>(claim.Value) ?? new List<string>();
                        return roles.Contains(role);
                    }
                    catch
                    {
                        return claim.Value == role;
                    }
                }
            }

            return false;
        }

        public async Task SignOut(string token)
        {
            var userId = await GetTokenUserId(token);
            if (_cache.CheckExistKeysStorage(userId, "accessTokens"))
                _cache.DeleteKeyFromStorage(userId, "accessTokens");
            else
                throw new Exception("Token not found in cache");
        }

        public async Task<UserLoginDTO?> RefreshSession(string accessToken, string refreshToken)
        {
            var validation = await ValidateTokenAsync(accessToken);
            if (validation == null)
                throw new Exception("Token validation failed");

            var userId = GetUserIdFromToken(validation);
            if (!userId.HasValue)
                throw new Exception("User Id not found in token");

            List<string> userRoles = new List<string>();
            var email = "none";

            foreach (var claim in validation.Claims)
            {
                if (claim.Type == "Email" || claim.Type == ClaimTypes.Email)
                    email = claim.Value;
                else if (claim.Type == "Role" || claim.Type == ClaimTypes.Role)
                {
                    try
                    {
                        userRoles = JsonSerializer.Deserialize<List<string>>(claim.Value) ?? new List<string>();
                    }
                    catch
                    {
                        userRoles = new List<string> { claim.Value };
                    }
                }
            }

            // ИЗМЕНИТЬ userId.Value на userId.Value (но тип уже Guid!)
            if (await IsRefreshValid(userId.Value, refreshToken))
            {
                var serializer_roles = JsonSerializer.Serialize(userRoles);
                var claims = new[]
                {
                    new Claim("Id", userId.Value.ToString()),
                    new Claim("Email", email),
                    new Claim("Role", serializer_roles),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var accessTokenNew = GenerateAccessToken(claims, userId.Value);
                var refreshTokenNew = GenerateRefreshToken(userId.Value);

                return new UserLoginDTO
                {
                    accessToken = accessTokenNew,
                    refreshToken = refreshTokenNew
                };
            }

            return null;
        }

        public async Task<TokenValidateResultDTO> AccessTokenValidation(string token)
        {
            try
            {
                var validation = await ValidateTokenAsync(token);
                if (validation == null)
                {
                    return new TokenValidateResultDTO { error_message = "Invalid token" };
                }

                var userId = GetUserIdFromToken(validation);
                if (!userId.HasValue)
                {
                    return new TokenValidateResultDTO { error_message = "User Id not found in token" };
                }

                List<string> roles = new List<string>();
                var email = "";

                foreach (var claim in validation.Claims)
                {
                    if (claim.Type == "Email" || claim.Type == ClaimTypes.Email)
                        email = claim.Value;
                    else if (claim.Type == "Role" || claim.Type == ClaimTypes.Role)
                    {
                        try
                        {
                            roles = JsonSerializer.Deserialize<List<string>>(claim.Value) ?? new List<string>();
                        }
                        catch
                        {
                            roles = new List<string> { claim.Value };
                        }
                    }
                }

                return new TokenValidateResultDTO
                {
                    token_success = new TokenSuccessDTO
                    {
                        Id = userId.Value, 
                        Email = email,
                        Roles = roles
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token validation error");
                return new TokenValidateResultDTO { error_message = ex.Message };
            }
        }
    }
}