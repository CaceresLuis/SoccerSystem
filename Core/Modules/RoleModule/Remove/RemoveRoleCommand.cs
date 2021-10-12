using MediatR;
using System;

namespace Core.Modules.RoleModule.Remove
{
    public class RemoveRoleCommand : IRequest<bool>
    {
        public String Id { get; set; }
    }
}
