using MediatR;
using Core.Dtos;

namespace Core.Modules.UserModule.Add
{
    public class AddUserCommand : IRequest<bool>
    {
        public UserDto UserDto { get; set; }
    }
}
