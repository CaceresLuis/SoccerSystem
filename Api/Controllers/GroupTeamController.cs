using MediatR;
using Core.Dtos;
using Core.Dtos.DtosApi;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.GroupTeamModule.Add;
using Core.Modules.GroupTeamModule.Remove;
using Microsoft.AspNetCore.Authorization;
using System;

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
            AddGroupTeamDto addGroupTeamDto = new AddGroupTeamDto { TeamId = addGroupTeam.IdGroup, IdGroup = addGroupTeam.IdGroup };
            return await _mediator.Send(new AddGroupTeamCommand { AddGroupTeamDto = addGroupTeamDto });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteGroupTeamEntity(Guid id)
        {
            await _mediator.Send(new RemoveGroupDetailCommand { Id = id });
            return true;
        }
    }
}
