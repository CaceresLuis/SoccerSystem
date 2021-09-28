using MediatR;
using Core.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.TeamModule.Get;
using Core.Modules.TeamModule.Add;
using Core.Modules.TeamModule.List;
using Core.Modules.TeamModule.Update;
using Core.Modules.TeamModule.Remove;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<TeamDto[]>> GetTeams()
        {
            return await _mediator.Send(new ListTeamsQuery());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TeamDto>> GetTeam(int id)
        {
            return await _mediator.Send(new GetTeamByIdQuery { TeamId = id });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> PutTeam(int id, [FromForm] TeamDto teamDto)
        {
            teamDto.Id = id;
            return await _mediator.Send(new UpdateTeamCommand { Team = teamDto });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> PostTeam([FromForm] TeamDto teamDto)
        {
            return await _mediator.Send(new AddTeamCommand { Team = teamDto });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> DeleteTeam(int id)
        {
            return await _mediator.Send(new RemoveTeamCommand { IdTeam = id });
        }
    }
}
