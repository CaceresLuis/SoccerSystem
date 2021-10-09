using System;
using MediatR;
using Core.Dtos;
using Core.Dtos.DtosApi;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Modules.TournamentModule.Add;
using Core.Modules.TournamentModule.Get;
using Core.Modules.TournamentModule.List;
using Microsoft.AspNetCore.Authorization;
using Core.Modules.TournamentModule.Remove;
using Core.Modules.TournamentModule.Update;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TournamentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<TournamentFullData[]>> GetTournaments()
        {
            return await _mediator.Send(new ListTournamentsQuery());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TournamentFullData>> GetTournament(Guid id)
        {
            return await _mediator.Send(new GetTournamentQuery { Id = id });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> PutTournamentEntity(Guid id, [FromForm] TournamentDto tournamentDto)
        {
            tournamentDto.Id = id;
            return await _mediator.Send(new UpdateTournamentCommnad { TournamentDto = tournamentDto });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> PostTournamentEntity([FromForm] AddTournamentDto tournamentDto)
        {
            return await _mediator.Send(new AddTournamentCommand { Tournament = tournamentDto });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<bool>> DeleteTournamentEntity(Guid id)
        {
            return await _mediator.Send(new RemoveTournamentCommand { Id = id });
        }
    }
}
