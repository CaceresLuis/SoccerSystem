using MediatR;

namespace Core.Modules.RoleModule.Add
{
    public class AddRoleCommand : IRequest<bool>
    {
        public string Name { get; set; }
    }
}
