using MediatR;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
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
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Not registered",
                        Message = $"The role: {request.Name} already exist",
                        Title = "Operation failed",
                        State = State.error,
                        IsSuccess = false
                    });

            await _roleRepository.AddRole(request.Name);

            return true;
        }
    }
}