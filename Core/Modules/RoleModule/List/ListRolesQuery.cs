using MediatR;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Modules.RoleModule.List
{
    public class ListRolesQuery : IRequest<List<IdentityRole>>
    {
    }
}
