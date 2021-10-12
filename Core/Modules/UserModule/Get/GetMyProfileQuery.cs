using MediatR;
using Core.Dtos;

namespace Core.Modules.UserModule.Get
{
    public class GetMyProfileQuery : IRequest<UserDto>
    {
        public string UserName { get; set; }
    }
}
