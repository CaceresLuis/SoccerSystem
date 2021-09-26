using MediatR;
using Core.Dtos;
using Core.Dtos.DtosApi;

namespace Core.Modules.UserModule.LoginApi
{
    public class LoginApiQuery : IRequest<UserDtoApi>
    {
        public LoginDto LoginDto { get; set; }
    }
}
