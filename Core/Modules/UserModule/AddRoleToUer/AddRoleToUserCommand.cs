using MediatR;

namespace Core.Modules.UserModule.AddRoleToUer
{
    public class AddRoleToUserCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
