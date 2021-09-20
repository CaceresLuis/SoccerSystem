using MediatR;

namespace Core.Modules.UserModule.Add
{
    public class AddRoleToUserCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
