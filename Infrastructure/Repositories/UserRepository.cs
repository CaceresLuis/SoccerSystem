using System;
using System.Linq;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
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


        public UserRepository(DataContext dataContext, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> AddUserAsync(UserEntity user)
        {
            IdentityResult addUser = await _userManager.CreateAsync(user, user.PasswordHash);
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

        public async Task<UserEntity> GetUser(string email, string password)
        {
            UserEntity user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new Exception("Error");

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!result.Succeeded)
                throw new Exception("Error");

            return user;
        }

        public async Task<UserEntity> GetByEmail(string email)
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

        public async Task<string> EditUser(UserEntity user)
        {
            IdentityResult save = await _userManager.UpdateAsync(user);
            if (!save.Succeeded)
                throw new Exception("Error");

            return $"El usuario con correo: {user.Email} se actualizo correctamente";
        }

        public async Task<bool> ChanguePassword(UserEntity user, string currentPassword, string newPassword)
        {
            IdentityResult save = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!save.Succeeded)
                throw new Exception("Error");

            return true;
        }

        public async Task<List<string>> GetRolesUser(UserEntity user)
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles == null)
                throw new Exception("Error");

            return userRoles.ToList();
        }
    }
}
