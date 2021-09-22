using MediatR;
using Core.Dtos;
using Infrastructure;
using Infrastructure.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.TeamModule.Get;
using Core.Modules.TeamModule.List;
using Core.Modules.TeamModule.Update;
using Core.Modules.TeamModule.Remove;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DataContext _context;

        public TeamController(DataContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<TeamDto[]>> GetTeams()
        {
            return await _mediator.Send(new ListTeamsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetTeam(int id)
        {
            return await _mediator.Send(new GetTeamByIdQuery { TeamId = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutTeam(int id, TeamDto teamDto)
        {
            teamDto.Id = id;
            return await _mediator.Send(new UpdateTeamCommand { Team = teamDto });
        }

        [HttpPost]
        public async Task<ActionResult<TeamEntity>> PostTeam(TeamEntity teamEntity)
        {
            _context.Teams.Add(teamEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeamEntity", new { id = teamEntity.Id }, teamEntity);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTeam(int id)
        {
            return await _mediator.Send(new RemoveTeamCommand { IdTeam = id });
        }
    }
}
