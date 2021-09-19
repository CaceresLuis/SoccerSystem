using MediatR;
using Core.Dtos;
using System.Collections.Generic;

namespace Core.Modules.UserModule.List
{
    public class ListUserQuery : IRequest<List<UserDto>>
    {
    }
}
