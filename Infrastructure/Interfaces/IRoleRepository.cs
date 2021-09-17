using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Interfaces
{
    public interface IRoleRepository
    {
        Task AddRole(string name);
        Task DeleteRole(IdentityRole role);
        Task<IdentityRole> GetRole(string name);
        Task<List<IdentityRole>> GetRoles();
    }
}
