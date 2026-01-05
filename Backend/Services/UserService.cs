using Backend.Interfaces;
using Backend.Middleware_Components.DTO;
using Backend.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using TechNest.Backend.Data;

namespace Backend.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(DataContext context, ILogger<UserService> logger)
            : base(context, logger) { }

        public async Task<(bool IsAuthenticated, string Message)> CheckUserAuth(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return (false, "Email не может быть пустым");

                if (string.IsNullOrWhiteSpace(password))
                    return (false, "Пароль не может быть пустым");

                if (password.Length < 5)
                    return (false, "Пароль должен содержать минимум 5 символов");

                if (password.Length > 20)
                    return (false, "Пароль не должен превышать 20 символов");

                try
                {
                    var mailAddress = new MailAddress(email);
                    if (!mailAddress.Address.Equals(email, StringComparison.OrdinalIgnoreCase))
                        return (false, "Неверный формат email");
                }
                catch
                {
                    return (false, "Неверный формат email");
                }

                var user = await _context.user_table
                    .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

                return user != null
                    ? (true, "Аутентификация успешна")
                    : (false, "Неверный email или пароль");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking user auth");
                return (false, "Ошибка при проверке аутентификации");
            }
        }

        public async Task<UserGetDTO> AddUser(UserAddDTO dtoObj)
        {
            try
            {
                var user = new UsersTable
                {
                    Id = dtoObj.Id(),
                    firstName = dtoObj.firstName,
                    lastName = dtoObj.lastName,
                    Email = dtoObj.Email,
                    Password = dtoObj.Password,
                    Roles = dtoObj.Roles,
                    Img = dtoObj.Img,
                    RegistrationDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    Status = dtoObj.Status,
                    created_at = DateTime.UtcNow
                };

                _context.user_table.Add(user);
                await _context.SaveChangesAsync();

                return MapToUserGetDTO(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user");
                throw;
            }
        }

        public async Task ChangeUser(Guid id, UserChangeDTO dtoObj)
        {
            try
            {
                var user = await _context.user_table.FindAsync(id);
                if (user == null)
                    throw new Exception($"User with id {id} not found");

                if (!string.IsNullOrEmpty(dtoObj.firstName))
                    user.firstName = dtoObj.firstName;

                if (!string.IsNullOrEmpty(dtoObj.lastName))
                    user.lastName = dtoObj.lastName;

                if (!string.IsNullOrEmpty(dtoObj.Email))
                    user.Email = dtoObj.Email;

                if (!string.IsNullOrEmpty(dtoObj.Password))
                    user.Password = dtoObj.Password;

                if (dtoObj.Roles != null && dtoObj.Roles.Length > 0)
                    user.Roles = string.Join(",", dtoObj.Roles);

                if (dtoObj.Img != null)
                    user.Img = dtoObj.Img;

                user.updated_at = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error changing user with id {id}");
                throw;
            }
        }

        public async Task DeleteUser(Guid idUser)
        {
            try
            {
                var user = await _context.user_table.FindAsync(idUser);
                if (user == null)
                    throw new Exception($"User with id {idUser} not found");

                _context.user_table.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting user with id {idUser}");
                throw;
            }
        }

        public async Task<List<UserGetDTO>> GetAllUsers(int from, int count)
        {
            try
            {
                var users = await _context.user_table
                    .OrderBy(u => u.created_at)
                    .Skip(from)
                    .Take(count)
                    .ToListAsync();

                return users.Select(MapToUserGetDTO).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all users");
                throw;
            }
        }

        public async Task<UserGetDTO?> GetUser(Guid idUser)
        {
            try
            {
                var user = await _context.user_table.FindAsync(idUser);
                return user != null ? MapToUserGetDTO(user) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting user with id {idUser}");
                throw;
            }
        }

        public async Task<UserGetDTO?> GetUserByEmail(string email)
        {
            try
            {
                var user = await _context.user_table
                    .FirstOrDefaultAsync(u => u.Email == email);

                return user != null ? MapToUserGetDTO(user) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting user by email: {email}");
                throw;
            }
        }

        public async Task<UserGetDTO?> GetUserById(int id)
        {
            try
            {
                var user = await _context.user_table
                    .FirstOrDefaultAsync(u => u.Id.ToString() == id.ToString());

                return user != null ? MapToUserGetDTO(user) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting user by id: {id}");
                throw;
            }
        }

        public async Task<List<Guid>> CollectAllIdUsers()
        {
            try
            {
                return await _context.user_table
                    .Select(u => u.Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error collecting all user ids");
                throw;
            }
        }

        public async Task<MeDTO> GetMeInfo(int userId)
        {
            try
            {
                var user = await _context.user_table
                    .FirstOrDefaultAsync(u => u.Id.ToString() == userId.ToString());

                if (user == null)
                    throw new Exception($"User with id {userId} not found");

                return new MeDTO
                {
                    Id = Convert.ToInt32(user.Id),
                    firstName = user.firstName,
                    lastName = user.lastName,
                    RegistrationDate = user.RegistrationDate,
                    Email = user.Email,
                    Password = user.Password,
                    Img = user.Img
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting me info for user id {userId}");
                throw;
            }
        }

        private UserGetDTO MapToUserGetDTO(UsersTable user)
        {
            return new UserGetDTO
            {
                Id = Convert.ToInt32(user.Id),
                firstName = user.firstName,
                lastName = user.lastName,
                RegistrationDate = user.RegistrationDate,
                Status = user.Status,
                Email = user.Email,
                Password = user.Password,
                Roles = user.Roles?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>(),
                Img = user.Img,
                created_at = user.created_at,
                updated_at = user.updated_at
            };
        }
    }
}