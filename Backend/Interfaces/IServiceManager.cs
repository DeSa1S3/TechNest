using Backend.Middleware_Components.DTO;
using Backend.Middleware_Components.Interfaces;
using Middleware_Components.DTO;

namespace Backend.Interfaces
{
    public interface IServicemanager
    {
        public Task AddNewUser(UserAddDTO dtoObj, string email, string token);

        public Task ChangeUser(UserChangeDTO dtoObj, Guid id, string token);

        public Task DeleteUser(Guid idUser, string token);

        public Task<List<UserGetDTO>>? GetAllUsers(int from, int count, string token);

        public Task<UserGetDTO?> GetUser(Guid idUser, string token);

        public Task<MeDTO> GetInfoMe(string token);
    }
}
