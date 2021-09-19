using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Core.Modules.RoleModule.Add
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public AddRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            IdentityRole exist = await _roleRepository.GetRole(request.Name);
            if (exist != null)
                throw new Exception("Error");

            await _roleRepository.AddRole(request.Name);

            return true;
        }
    }
}