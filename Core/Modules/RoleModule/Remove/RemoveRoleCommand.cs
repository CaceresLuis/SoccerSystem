using MediatR;

namespace Core.Modules.RoleModule.Remove
{
    public class RemoveRoleCommand : IRequest<bool>
    {
        public string Name { get; set; }
    }
}
