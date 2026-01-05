using Backend.Middleware_Components.DTO;

namespace Backend.Interfaces
{
    public interface IUserService
    {
        Task<(bool IsAuthenticated, string Message)> CheckUserAuth(string email, string password);
        Task<UserGetDTO> AddUser(UserAddDTO dtoObj);
        Task ChangeUser(Guid id, UserChangeDTO dtoObj);
        Task DeleteUser(Guid idUser);
        Task<List<UserGetDTO>> GetAllUsers(int from, int count);
        Task<UserGetDTO?> GetUser(Guid idUser);
        Task<UserGetDTO?> GetUserByEmail(string email);
        Task<UserGetDTO?> GetUserById(int id);
        Task<List<Guid>> CollectAllIdUsers();
        Task<MeDTO> GetMeInfo(int userId);
    }
}
