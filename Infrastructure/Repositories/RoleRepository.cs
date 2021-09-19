﻿using System;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;

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
            IdentityResult create = await _roleManager.CreateAsync(new IdentityRole(name));
            if (!create.Succeeded)
                throw new Exception("Error");
        }

        public async Task<IdentityRole> GetRole(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task DeleteRole(IdentityRole role)
        {
            IdentityResult delete = await _roleManager.DeleteAsync(role);
            if (!delete.Succeeded)
                throw new Exception("Error");
        }
    }
}