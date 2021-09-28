using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Modules.RoleModule.List
{
    public class ListRolesHandler : IRequestHandler<ListRolesQuery, List<IdentityRole>>
    {
        private readonly IRoleRepository _roleRepository;

        public ListRolesHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<IdentityRole>> Handle(ListRolesQuery request, CancellationToken cancellationToken)
        {
            List<IdentityRole> roles = await _roleRepository.GetRoles();
            return roles;
        }
    }
}
