using Infrastructure.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddRoleToUser(UserEntity user, string role);
        Task<bool> AddUserAsync(UserEntity user, string pass);
        Task<bool> ChanguePassword(UserEntity user, string currentPassword, string newPassword);
        Task<bool> UpdateUserAsync(UserEntity user);
        Task<UserEntity> FindByEmailAsync(string email);
        string GetSessionUser();
        Task<UserEntity> FindUserByName(string userId);
        Task<UserEntity> GetUserInSesscion();
        Task<SignInResult> LoginAsync(string userName, string password, bool rememberMe);
        Task LogoutAsync();
        Task<List<string>> GetUserRolesAsync(UserEntity user);
        Task<List<UserEntity>> GetUsersAsync();
        Task<bool> LoginApiAsync(UserEntity user, string password);
    }
}
