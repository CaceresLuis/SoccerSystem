using MediatR;
using Core.Dtos;

namespace Core.Modules.UserModule.Get
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public string Email { get; set; }
    }
}
