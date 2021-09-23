using MediatR;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Models;
using Core.Modules.TournamentModule.List;
using Core.Dtos;
using Core.Modules.TournamentModule.Add;
using Core.Dtos.DtosApi;
using Shared.Helpers.Image;
using AutoMapper;
using Core.Modules.TournamentModule.Get;
using Core.Modules.TournamentModule.Update;
using Core.Modules.TournamentModule.Remove;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IIMageHelper _iMageHelper;
        private readonly DataContext _context;

        public TournamentController(DataContext context, IMediator mediator, IIMageHelper iMageHelper, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _iMageHelper = iMageHelper;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<TournamentFullData[]>> GetTournaments()
        {
            return await _mediator.Send(new ListTournamentsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentFullData>> GetTournament(int id)
        {
            return await _mediator.Send(new GetTournamentQuery { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutTournamentEntity(int id, [FromForm] TournamentDto tournamentDto)
        {
            tournamentDto.Id = id;
            return await _mediator.Send(new UpdateTournamentCommnad { TournamentDto = tournamentDto });
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostTournamentEntity([FromForm] AddTournamentDto tournamentDto)
        {
            return await _mediator.Send(new AddTournamentCommand { Tournament = tournamentDto });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTournamentEntity(int id)
        {
            return await _mediator.Send(new RemoveTournamentCommand { Id = id });
        }
    }
}
