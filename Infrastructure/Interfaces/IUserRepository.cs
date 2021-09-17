using Infrastructure.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddRoleToUser(UserEntity user, string role);
        Task<bool> AddUserAsync(UserEntity user);
        Task<bool> ChanguePassword(UserEntity user, string currentPassword, string newPassword);
        Task<string> EditUser(UserEntity user);
        Task<bool> EmailExist(string email);
        Task<UserEntity> GetByEmail(string email);
        Task<List<string>> GetRolesUser(UserEntity user);
        Task<UserEntity> GetUser(string email, string password);
        Task<UserEntity> GetUserById(string userId);
        Task<UserEntity> GetUserName(string userName);
        Task<UserEntity> UserNameExist(string userName);
    }
}
