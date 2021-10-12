using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Core.Modules.RoleModule.Add;
using Core.Modules.RoleModule.List;
using Microsoft.AspNetCore.Identity;
using Core.Modules.RoleModule.Remove;
using Microsoft.AspNetCore.Authorization;
using Core.Modules.UserModule.AddRoleToUer;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostRol(AddRoleCommand data)
        {
            return await _mediator.Send(data);
        }

        [HttpPost("AddRoleToUser")]
        public async Task<ActionResult<bool>> AddRoleToUser(AddRoleToUserCommand data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<IdentityRole>>> GetRoles()
        {
            return await _mediator.Send(new ListRolesQuery());
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<bool>> DeleteRole(RemoveRoleCommand data)
        {
            return await _mediator.Send(data);
        }
    }
}
