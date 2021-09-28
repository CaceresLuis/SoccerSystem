using System;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task AddRole(string name)
        {
            await _roleManager.CreateAsync(new IdentityRole(name));
        }

        public async Task<IdentityRole> GetRole(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }

        public async Task<IdentityRole> FindRole(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<bool> DeleteRole(IdentityRole role)
        {
            IdentityResult delete = await _roleManager.DeleteAsync(role);
            return delete.Succeeded;
        }
    }
}
