using MediatR;
using Core.Dtos.DtosApi;

namespace Core.Modules.UserModule.GetUserSession
{
    public class GetUserSessionCommand : IRequest<UserDtoApi>
    {
    }
}
