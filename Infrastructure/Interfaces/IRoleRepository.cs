using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Interfaces
{
    public interface IRoleRepository
    {
        Task AddRole(string name);
        Task<bool> DeleteRole(IdentityRole role);
        Task<IdentityRole> FindRole(string id);
        Task<IdentityRole> GetRole(String name);
        Task<List<IdentityRole>> GetRoles();
    }
}
