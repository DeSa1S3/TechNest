using Backend.Middleware_Components.DTO;
using Backend.Middleware_Components.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Backend.Middleware_Components.JWT
{
    public class JwtTokensService : IJwtTokensService
    {
        private readonly IConfiguration _config;
        private readonly ICacheService _cache;
        private readonly ILogger _logger;

        public JwtTokensService(ICacheService cache, IConfiguration config)
        {
            _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("jwt-logger");
            _config = config;
            _cache = cache;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims, int id)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings")["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config.GetSection("JwtSettings")["Issuer"],
                audience: _config.GetSection("JwtSettings")["Audience"],
                claims: claims,
                expires: null,
                signingCredentials: creds
            );

            var tokenReleased = new JwtSecurityTokenHandler().WriteToken(token);


            if (_cache.CheckExistKeysStorage(id, "accessTokens"))
                _cache.DeleteKeyFromStorage(id, "accessTokens");

            _cache.WriteKeyInStorage(id, "accessTokens", tokenReleased, DateTime.UtcNow.AddMinutes(double.Parse(_config.GetSection("JwtSettings")["ExpiryInMinutesAccess"])));

            _logger.LogInformation($"Сгенерирован токен доступа для {id}: {tokenReleased}");

            return tokenReleased;
        }

        public string GenerateRefreshToken(int id)
        {
            var randomBytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            var genToken = Convert.ToBase64String(randomBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", ""); ;

            if (_cache.CheckExistKeysStorage(id, "refreshTokens"))
                _cache.DeleteKeyFromStorage(id, "refreshTokens");

            _cache.WriteKeyInStorage(id, "refreshTokens", genToken, DateTime.UtcNow.AddDays(double.Parse(_config.GetSection("JwtSettings")["ExpiryInDaysRefresh"])));

            _logger.LogInformation($"Сгенерирован токен обновления для {id}: {genToken}");

            return genToken;
        }

        private async Task<JwtSecurityToken?> ValidateTokenAsync(string? token)
        {
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogWarning("Token is null or empty");
                return null;
            }

            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _config.GetSection("JwtSettings")["Issuer"],
                ValidAudience = _config.GetSection("JwtSettings")["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings")["SecretKey"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                RequireSignedTokens = true,
                RequireExpirationTime = false,
                ValidateLifetime = false
            };

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                tokenHandler.ValidateToken(token, validationParameters, out var rawValidatedToken);

                return (JwtSecurityToken)rawValidatedToken;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> IsRefreshValid(int id, string token)
        {
            if (_cache.CheckExistKeysStorage(id, "refreshTokens"))
            {
                var tokenFromCache = _cache.GetKeyFromStorage(id, "refreshTokens");

                return tokenFromCache != null && tokenFromCache == token;
            }

            return false;
        }

        public async Task<bool> IsAccessValid(string? token)
        {
            var validation = await ValidateTokenAsync(token);

            var userId = -1;

            if (validation == null)
                throw new Exception("validation failed");

            foreach (var claim in validation.Claims)
            {
                if (claim.Type == "Id")
                    userId = int.Parse(claim.Value);
            }

            if (_cache.CheckExistKeysStorage(userId, "accessTokens"))
            {
                var tokenFromCache = _cache.GetKeyFromStorage(userId, "accessTokens");

                return tokenFromCache != null && tokenFromCache == token;
            }

            return false;
        }

        public async Task<int> GetTokenUserId(string token)
        {
            var validation = await ValidateTokenAsync(token);

            var userId = -1;

            if (validation == null)
                throw new Exception("validation failed");

            foreach (var claim in validation.Claims)
            {
                if (claim.Type == "Id")
                    userId = int.Parse(claim.Value);
            }

            return userId;
        }

        public async Task<bool> RoleValid(string token, string role)
        {
            var validation = await ValidateTokenAsync(token);

            var roleJwt = "none";

            if (validation == null)
                throw new Exception("validation failed");

            foreach (var claim in validation.Claims)
            {
                if (claim.Type == "Role")
                    roleJwt = claim.Value;
            }

            return roleJwt != "none" && roleJwt == role;
        }  

        public async Task SignOut(string token)
        {
            var userId = await GetTokenUserId(token);

            if (_cache.CheckExistKeysStorage(userId, "accessTokens"))
                _cache.DeleteKeyFromStorage(userId, "accessTokens");
            else
                throw new Exception("TokenNotFound");
        }

        public async Task<UserLoginDTO?> RefreshSession(string accessToken, string refreshToken)
        {
            var validation = await ValidateTokenAsync(accessToken);

            var userId = -1;

            List<string> userRoles = new List<string>();

            var email = "none";

            if (validation == null)
                throw new Exception("validation failed");

            foreach (var claim in validation.Claims)
            {
                if (claim.Type == "Id")
                    userId = int.Parse(claim.Value);

                if (claim.Type == "Role")
                    userRoles = JsonSerializer.Deserialize<List<string>>(claim.Value);

                if (claim.Type == "Email")
                    email = claim.Value;
            }


            if (_cache.CheckExistKeysStorage(userId, "refreshTokens"))
            {
                var refreshFromCache = _cache.GetKeyFromStorage(userId, "refreshTokens");

                if (refreshFromCache == refreshToken)
                {

                    var serializer_roles = JsonSerializer.Serialize(userRoles);

                    var claims = new[]
                    {
                        new Claim("Id", userId.ToString()),
                        new Claim("Email", email),
                        new Claim("Role", serializer_roles),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };


                    var accessTokenNew = GenerateAccessToken(claims, userId);

                    var refreshTokenNew = GenerateRefreshToken(userId);

                    return new UserLoginDTO
                    {
                        accessToken = accessTokenNew,
                        refreshToken = refreshTokenNew
                    };
                }
            }

            return null;
        }
    }
}
