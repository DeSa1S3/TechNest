using Backend.Interfaces;
using Backend.Middleware_Components.DTO;
using Backend.Middleware_Components.Interfaces;
using Middleware_Components.DTO;

namespace Backend.Services
{
    public class ServiceManager : IServicemanager
    {
        private readonly IDatabaseService _database;
        private readonly IJwtTokensService _jwt;
        private readonly ICacheService _cache;
        private readonly ILogger _logger;

        public ServiceManager(IDatabaseService database, IJwtTokensService jwt, ICacheService cache)
        {
            _logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("service-manager-logger");
            _database = database;
            _jwt = jwt;
            _cache = cache;
        }

        //USERS CONTROLS PARTS
        public async Task AddNewUser(UserAddDTO dtoObj, string email, string token)
        {
            var validation = await _jwt.IsAccessValid(token);

            if (!validation)
            {
                throw new Exception("token_invalid");
            }
            else if (validation)
            {
                var isRole = await _jwt.RoleValid(token, "MANAGER");


                if (isRole)
                {



                    //var userData = await _database.AddUser(dtoObj);

                    //await _mail.SendRegisterData(email, userData);
                }
                else
                    throw new Exception("role_invalid");
            }
        }

        public async Task ChangeUser(UserChangeDTO dtoObj, Guid id, string token)
        {
            var validation = await _jwt.IsAccessValid(token);

            if (!validation)
            {
                throw new Exception("token_invalid");
            }
            else if (validation)
            {
                var isRole = await _jwt.RoleValid(token, "MANAGER");

                //if (isRole)
                //    await _database.ChangeUser(idUser);
                //else
                //    throw new Exception("role_invalid");
            }
        }

        public async Task DeleteUser(Guid idUser, string token)
        {
            var validation = await _jwt.IsAccessValid(token);

            if (!validation)
            {
                throw new Exception("token_invalid");
            }
            else if (validation)
            {
                
                var isRole = await _jwt.RoleValid(token, "MANAGER");

                //if (isRole)
                //    await _database.DeleteUser(idUser);
                //else
                //    throw new Exception("role_invalid");
            }
        }

        public async Task<List<UserGetDTO>>? GetAllUsers(int from, int count, string token)
        {
            var validation = await _jwt.IsAccessValid(token);

            if (!validation)
            {
                throw new Exception("token_invalid");
            }
            else if (validation)
            {
                // return await _database.GetAllUsers(from, count);
            }

            return null;
        }

        public async Task<UserGetDTO?> GetUser(Guid idUser, string token)
        {
            var validation = await _jwt.IsAccessValid(token);

            if (!validation)
            {
                throw new Exception("token_invalid");
            }
            else if (validation)
            {
              //  return await _database.GetUser(idUser);
            }

            return null;
        }

        //public async Task CreateRuleTimed(AddRuleDTO dtoObj, string token)
        //{
        //    List<TimedRuleDTO> rulesForApprove = new List<TimedRuleDTO>();

        //    var validation = await _jwt.AccessTokenValidation(token);

        //    if (validation.TokenHasError())
        //    {
        //        throw new Exception("token_invalid");
        //    }
        //    else if (validation.TokenHasSuccess())
        //    {
        //        try
        //        {
        //            var rulesIfCached = _cache.GetKeyFromStorage<List<TimedRuleDTO>>($"rule_for_alert_{validation.token_success!.Id}");

        //            if (rulesIfCached != null)
        //                rulesForApprove = rulesIfCached;

        //            TimedRuleDTO rulesTableToCache = new TimedRuleDTO()
        //            {
        //                name = dtoObj.name,
        //                description = dtoObj.description,
        //                logic = dtoObj.logic,
        //                severity = dtoObj.severity,
        //                status = dtoObj.status,
        //                created_by = validation.token_success!.Id,
        //                created_at = DateTime.UtcNow
        //            };

        //            rulesForApprove.Add(rulesTableToCache);

        //            _logger.LogInformation($"ID TOKEN SUCCESS: {validation.token_success!.Id}");

        //            _cache.WriteKeyInStorageObject<List<TimedRuleDTO>>(
        //                $"rule_for_alert_{validation.token_success!.Id}",
        //                rulesForApprove,
        //                DateTime.UtcNow.AddDays(1)
        //            );
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message);
        //        }
        //    }
        //}

        public async Task<MeDTO> GetInfoMe(string token)
        {
            var validation = await _jwt.IsAccessValid(token);

            if (!validation)
            {
                throw new Exception("token_invalid");
            }
            else if (validation)
            {
                var userId = _jwt.GetTokenUserId(token);


             //   return await _database.GetMeInfo(validation.token_success.Id);
            }

            return null;
        }
    }
}
