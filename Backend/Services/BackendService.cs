using Backend.Interfaces;
using Backend.Middleware_Components.DTO;
using Backend.Middleware_Components.Interfaces;
using Microsoft.Extensions.Logging;
using Middleware_Components.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class BackendService : IBackendService
    {
        private readonly IJwtTokensService _jwt;
        private readonly ICacheService _cache;
        private readonly IDatabaseService _database;
        private readonly ILogger<BackendService> _logger;

        public BackendService(
            IJwtTokensService jwt,
            ICacheService cache,
            IDatabaseService database,
            ILogger<BackendService> logger)
        {
            _jwt = jwt;
            _cache = cache;
            _database = database;
            _logger = logger;
        }

        public async Task<string?> ClientSignOut(string bearer_key)
        {
            var validation = await _jwt.AccessTokenValidation(bearer_key);

            if (validation.TokenHasError())
            {
                return null;
            }
            else if (validation.TokenHasSuccess())
            {
                if (validation.token_success != null)
                {
                    _cache.DeleteKeyFromStorage(validation.token_success.Id, "accessTokens");
                    _cache.DeleteKeyFromStorage(validation.token_success.Id, "refreshTokens");

                    _logger.LogInformation($"Пользователь id: {validation.token_success.Id} вышел!");
                    return $"{validation.token_success.Id}_is_logout";
                }
            }

            return null;
        }

        public async Task<AuthTokenInfo?> AuthPart(string username, string password)
        {
            try
            {
                var checkSuccess = await _database.CheckUserAuth(username, password);

                if (!checkSuccess)
                {
                    return new AuthTokenInfo { error = "Invalid credentials" };
                }

                // Получаем информацию о пользователе
                var userInfo = await _database.GetUserByEmail(username);

                // Генерируем токены через IJwtTokensService
                var claims = new List<System.Security.Claims.Claim>
                {
                    new System.Security.Claims.Claim("Id", userInfo.Id.ToString()),
                    new System.Security.Claims.Claim("Email", userInfo.Email),
                    new System.Security.Claims.Claim("Role", System.Text.Json.JsonSerializer.Serialize(userInfo.Roles))
                };

                var accessToken = _jwt.GenerateAccessToken(claims, userInfo.Id);
                var refreshToken = _jwt.GenerateRefreshToken(userInfo.Id);

                return new AuthTokenInfo
                {
                    accessToken = accessToken,
                    refreshToken = refreshToken,
                    expires_at = DateTime.UtcNow.AddMinutes(15)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AuthPart");
                return new AuthTokenInfo { error = ex.Message };
            }
        }

        public async Task AddNewUser(UserAddDTO dtoObj, string email, string token)
        {
            var validation = await _jwt.IsAccessValid(token);

            if (!validation)
            {
                throw new Exception("token_invalid");
            }

            var isRole = await _jwt.RoleValid(token, "MANAGER");

            if (!isRole)
            {
                throw new Exception("role_invalid");
            }

            var userData = await _database.AddUser(dtoObj);
            // _mail.SendRegisterData(email, userData); // Раскомментируйте при наличии IMailService
        }

        public async Task ChangeUser(UserChangeDTO dtoObj, Guid id, string token)
        {
            var validation = await _jwt.IsAccessValid(token);

            if (!validation)
            {
                throw new Exception("token_invalid");
            }

            var isRole = await _jwt.RoleValid(token, "MANAGER");

            if (!isRole)
            {
                throw new Exception("role_invalid");
            }

            await _database.ChangeUser(id, dtoObj);
        }

        public async Task DeleteUser(Guid idUser, string token)
        {
            var validation = await _jwt.IsAccessValid(token);

            if (!validation)
            {
                throw new Exception("token_invalid");
            }

            var isRole = await _jwt.RoleValid(token, "MANAGER");

            if (!isRole)
            {
                throw new Exception("role_invalid");
            }

            await _database.DeleteUser(idUser);
        }

        public async Task<List<UserGetDTO>> GetAllUsers(int from, int count, string token)
        {
            var validation = await _jwt.IsAccessValid(token);

            if (!validation)
            {
                throw new Exception("token_invalid");
            }

            return await _database.GetAllUsers(from, count);
        }

        public async Task<UserGetDTO?> GetUser(Guid idUser, string token)
        {
            var validation = await _jwt.IsAccessValid(token);

            if (!validation)
            {
                throw new Exception("token_invalid");
            }

            return await _database.GetUser(idUser);
        }

        public async Task CreateRuleTimed(AddRuleDTO dtoObj, string token)
        {
            var validation = await _jwt.AccessTokenValidation(token);

            if (validation.TokenHasError())
            {
                throw new Exception("token_invalid");
            }
            else if (validation.TokenHasSuccess())
            {
                try
                {
                    var rulesForApprove = new List<TimedRuleDTO>();
                    var rulesIfCached = _cache.GetKeyFromStorage<List<TimedRuleDTO>>($"rule_for_alert_{validation.token_success!.Id}");

                    if (rulesIfCached != null)
                        rulesForApprove = rulesIfCached;

                    var rulesTableToCache = new TimedRuleDTO()
                    {
                        name = dtoObj.name,
                        description = dtoObj.description,
                        logic = dtoObj.logic,
                        severity = dtoObj.severity,
                        status = dtoObj.status,
                        created_by = new Guid(validation.token_success!.Id.ToString()),
                        created_at = DateTime.UtcNow
                    };

                    rulesForApprove.Add(rulesTableToCache);

                    _logger.LogInformation($"ID TOKEN SUCCESS: {validation.token_success!.Id}");

                    _cache.WriteKeyInStorageObject<List<TimedRuleDTO>>(
                        $"rule_for_alert_{validation.token_success!.Id}",
                        rulesForApprove,
                        DateTime.UtcNow.AddDays(1)
                    );
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public async Task<List<List<TimedRuleDTO>?>> GetTimedRules(string token)
        {
            var validation = await _jwt.AccessTokenValidation(token);

            if (validation.TokenHasError())
            {
                throw new Exception("token_invalid");
            }
            else if (validation.TokenHasSuccess())
            {
                var allRulesCached = new List<List<TimedRuleDTO>?>();
                var userIds = await _database.CollectAllIdUsers();

                foreach (var userId in userIds)
                {
                    var rulesCached = _cache.GetKeyFromStorage<List<TimedRuleDTO>>($"rule_for_alert_{userId}");
                    allRulesCached.Add(rulesCached);
                }

                return allRulesCached;
            }

            throw new Exception("token_validation_failed");
        }

        public async Task AcceptRule(string ruleName, Guid idUser, string token)
        {
            var validation = await _jwt.AccessTokenValidation(token);

            if (validation.TokenHasError())
            {
                throw new Exception("token_invalid");
            }
            else if (validation.TokenHasSuccess())
            {
                var rulesIfCached = _cache.GetKeyFromStorage<List<TimedRuleDTO>>($"rule_for_alert_{idUser}");

                if (rulesIfCached != null)
                {
                    foreach (var rule in rulesIfCached)
                    {
                        if (rule.name == ruleName)
                        {
                            await _database.RuleFillUp(rule);
                            return;
                        }
                    }
                    throw new Exception("rule_not_found");
                }
                else
                {
                    throw new Exception("rule_not_found");
                }
            }
        }

        public async Task<GetRuleDTO?> GetRuleFromDB(Guid ruleId, string token)
        {
            var validation = await _jwt.AccessTokenValidation(token);

            if (validation.TokenHasError())
            {
                throw new Exception("token_invalid");
            }
            else if (validation.TokenHasSuccess())
            {
                return await _database.GetRuleFromDB(ruleId);
            }

            return null;
        }

        public async Task<List<GetRuleDTO>?> GetRulesFromDB(string token)
        {
            var validation = await _jwt.AccessTokenValidation(token);

            if (validation.TokenHasError())
            {
                throw new Exception("token_invalid");
            }
            else if (validation.TokenHasSuccess())
            {
                return await _database.GetAllRulesFromDB();
            }

            return null;
        }

        public async Task<MeDTO> GetInfoMe(string token)
        {
            var validation = await _jwt.IsAccessValid(token);

            if (!validation)
            {
                throw new Exception("token_invalid");
            }

            var userId = await _jwt.GetTokenUserId(token);
            return await _database.GetMeInfo(userId);
        }

        public async Task<AuthTokenInfo?> RefreshClientSession(string refreshTokenDTO)
        {
            // Реализация обновления сессии
            try
            {
                // Парсим refresh token
                var refreshTokenData = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(refreshTokenDTO);
                if (refreshTokenData == null || !refreshTokenData.ContainsKey("refreshToken"))
                {
                    throw new Exception("Invalid refresh token format");
                }

                var refreshToken = refreshTokenData["refreshToken"];

                // Получаем userId из токена (в реальности нужен доступ к токену)
                var userId = await _jwt.GetTokenUserId(refreshToken);

                // Проверяем валидность refresh токена
                if (await _jwt.IsRefreshValid(userId, refreshToken))
                {
                    // Получаем информацию о пользователе
                    var userInfo = await _database.GetUserById(userId);

                    // Генерируем новые токены
                    var claims = new List<System.Security.Claims.Claim>
                    {
                        new System.Security.Claims.Claim("Id", userInfo.Id.ToString()),
                        new System.Security.Claims.Claim("Email", userInfo.Email),
                        new System.Security.Claims.Claim("Role", System.Text.Json.JsonSerializer.Serialize(userInfo.Roles))
                    };

                    var accessToken = _jwt.GenerateAccessToken(claims, userInfo.Id);
                    var newRefreshToken = _jwt.GenerateRefreshToken(userInfo.Id);

                    return new AuthTokenInfo
                    {
                        accessToken = accessToken,
                        refreshToken = newRefreshToken,
                        expires_at = DateTime.UtcNow.AddMinutes(15)
                    };
                }

                throw new Exception("Invalid refresh token");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in RefreshClientSession");
                return new AuthTokenInfo { error = ex.Message };
            }
        }
    }
}