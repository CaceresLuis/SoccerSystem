using System;
using System.Linq;
using Infrastructure.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserRepository(DataContext dataContext, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SignInResult> LoginAsync(string userName, string password, bool rememberMe)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);
        }

        public string GetSessionUser()
        {
            return _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(u => u.Type == ClaimTypes.Name)?.Value;
        }

        public async Task<UserEntity> GetUserInSesscion()
        {
            UserEntity user = await _userManager.FindByNameAsync(GetSessionUser());
            if (user == null)
                throw new Exception("Error");

            return user;
        }

        public async Task<List<string>> GetUserRolesAsync(UserEntity user)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            if (roles.Count() < 1)
               return new List<string>();

            return roles.ToList();
        }

        public async Task<List<UserEntity>> GetUsersAsync()
        {
            return await _dataContext.Users.ToListAsync();
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> AddUserAsync(UserEntity user, string pass)
        {
            IdentityResult addUser = await _userManager.CreateAsync(user, pass);
            if (!addUser.Succeeded)
                return false;

            return true;
        }

        public async Task<bool> AddRoleToUser(UserEntity user, string role)
        {
            IdentityResult add = await _userManager.AddToRoleAsync(user, role);
            if (!add.Succeeded)
                return false;

            return true;
        }


        public async Task<UserEntity> GetByEmailAsync(string email)
        {
            UserEntity user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<UserEntity> GetUserName(string userName)
        {
            UserEntity user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            return user;
        }

        public async Task<UserEntity> GetUserById(string userId)
        {
            UserEntity user = await _dataContext.Users.FindAsync(userId);

            return user;
        }

        public async Task<UserEntity> UserNameExist(string userName)
        {
            UserEntity exist = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            return exist;
        }

        public async Task<bool> EmailExist(string email)
        {
            bool exist = await _dataContext.Users.Where(u => u.Email == email).AnyAsync();
            if (exist)
                throw new Exception("Error");

            return exist;
        }

        public async Task<bool> UpdateUserAsync(UserEntity user)
        {
            IdentityResult save = await _userManager.UpdateAsync(user);
            if (!save.Succeeded)
                throw new Exception("Error");

            return true;
        }

        public async Task<bool> ChanguePassword(UserEntity user, string currentPassword, string newPassword)
        {
            IdentityResult save = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!save.Succeeded)
                throw new Exception("Error");

            return true;
        }
    }
}
