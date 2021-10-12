using System;
using MediatR;
using Core.Dtos.AddDtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.GroupTeamModule.Add;
using Microsoft.AspNetCore.Authorization;
using Core.Modules.GroupTeamModule.Remove;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class GroupTeamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupTeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostGroupTeamEntity(AddGroupTeam addGroupTeam)
        {
            return await _mediator.Send(new AddGroupTeamCommand { AddGroupTeam = addGroupTeam });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteGroupTeamEntity(Guid id)
        {
            await _mediator.Send(new RemoveGroupDetailCommand { Id = id });
            return true;
        }
    }
}
