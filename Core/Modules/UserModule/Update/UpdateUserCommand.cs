using MediatR;
using Core.Dtos;

namespace Core.Modules.UserModule.Update
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UserDto UserDto { get; set; }
    }
}
