using MediatR;
using System;

namespace Core.Modules.RoleModule.Remove
{
    public class RemoveRoleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
