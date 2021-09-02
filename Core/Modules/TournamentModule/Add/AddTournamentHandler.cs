using MediatR;
using AutoMapper;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.Add
{
    public class AddTournamentHandler : IRequestHandler<AddTournamentCommand, ActionResponse>
    {
        private readonly IMapper _mapper;

        private readonly ITournamentRepository _tournamentRepository;

        public AddTournamentHandler(ITournamentRepository tournamentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<ActionResponse> Handle(AddTournamentCommand request, CancellationToken cancellationToken)
        {
            TournamentEntity tournamentByName = await _tournamentRepository.GetTournamentByNameAsync(request.Tournament.Name);
            if (tournamentByName != null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"The {tournamentByName.Name} tournament name is already registered", State = State.error };

            TournamentEntity tournament = _mapper.Map<TournamentEntity>(request.Tournament);

            bool create = await _tournamentRepository.AddTournamentAsync(tournament);
            if(!create)
                return new ActionResponse { IsSuccess = false, Title = "Error!", Message = $"The tournament {tournament.Name} was not created", State = State.error };

            return new ActionResponse { IsSuccess = true, Title = "Created", Message = $"The tournament {tournament.Name} was created", State = State.success };
        }
    }
}
