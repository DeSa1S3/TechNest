using Backend.Middleware_Components.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Interfaces
{
    public interface IUserService
    {
        Task<(bool IsAuthenticated, string Message)> CheckUserAuth(string email, string password);
        Task<UserGetDTO> AddUser(UserAddDTO dtoObj, string email, string authHeader);
        Task ChangeUser(UserChangeDTO dtoObj, Guid id, string authHeader);
        Task DeleteUser(Guid idUser, string authHeader);
        Task<List<UserGetDTO>> GetAllUsers(int from, int count, string authHeader);
        Task<UserGetDTO?> GetUser(Guid idUser, string authHeader);
        Task<UserGetDTO?> GetUserByEmail(string email);
        Task<UserGetDTO?> GetUserById(int id);
        Task<List<Guid>> CollectAllIdUsers();
        Task<MeDTO> GetMeInfo(int userId);
    }
}