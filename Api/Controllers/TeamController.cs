using MediatR;
using Core.Dtos;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.TeamModule.Get;
using Core.Modules.TeamModule.Add;
using Core.Modules.TeamModule.List;
using Core.Modules.TeamModule.Update;
using Core.Modules.TeamModule.Remove;
using Microsoft.AspNetCore.Authorization;
using Shared.Helpers.Image;
using Core.Modules.ImageModule.Add;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IIMageHelper _iMageHelper;

        public TeamController(IMediator mediator, IIMageHelper iMageHelper)
        {
            _mediator = mediator;
            _iMageHelper = iMageHelper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<TeamDto[]>> GetTeams()
        {
            return await _mediator.Send(new ListTeamsQuery());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TeamDto>> GetTeam(Guid id)
        {
            return await _mediator.Send(new GetTeamByIdQuery { TeamId = id });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> PutTeam(Guid id, [FromForm] TeamDto teamDto)
        {
            teamDto.Id = id;
            return await _mediator.Send(new UpdateTeamCommand { Team = teamDto });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> PostTeam([FromForm] TeamDto teamDto)
        {
            await _mediator.Send(new AddTeamCommand { Team = teamDto });

            if(teamDto.LogoFile != null)
            {
                TeamDto team = await _mediator.Send(new GetTeamByNameQuery { TeamName = teamDto.Name });
                ImageData img = new ImageData { File = teamDto.LogoFile, Reference = team.Id, Folder = "Teams" };
                await _mediator.Send(new AddImageCommad { ImageData = img });
            }

            return true;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> DeleteTeam(Guid id)
        {
            return await _mediator.Send(new RemoveTeamCommand { IdTeam = id });
        }
    }
}
