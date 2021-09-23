using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Core.Modules.RoleModule.Remove
{
    public class RemoveRoleHandler : IRequestHandler<RemoveRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public RemoveRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            IdentityRole rol = await _roleRepository.GetRole(request.Name);
            return await _roleRepository.DeleteRole(rol);
        }
    }
}
