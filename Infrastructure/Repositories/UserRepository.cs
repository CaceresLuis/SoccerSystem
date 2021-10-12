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

        public async Task<bool> LoginApiAsync(UserEntity user, string password)
        {
            SignInResult login = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return login.Succeeded;
        }

        public string GetSessionUser()
        {
            return _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public async Task<UserEntity> GetUserInSesscion()
        {
            return await _userManager.FindByNameAsync(GetSessionUser());
        }
        
        public async Task<UserEntity> GetUserInSesscion(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
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

        public async Task<IdentityResult> AddUserAsync(UserEntity user, string pass)
        {

            return await _userManager.CreateAsync(user, pass);
        }

        public async Task<bool> AddRoleToUser(UserEntity user, string role)
        {
            IdentityResult add = await _userManager.AddToRoleAsync(user, role);
            return add.Succeeded;
        }

        public async Task<UserEntity> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<UserEntity> FindUserByName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<bool> UpdateUserAsync(UserEntity user)
        {
            IdentityResult save = await _userManager.UpdateAsync(user);

            return save.Succeeded;
        }

        public async Task<bool> ChanguePassword(UserEntity user, string currentPassword, string newPassword)
        {
            IdentityResult save = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            return save.Succeeded;
        }
    }
}
