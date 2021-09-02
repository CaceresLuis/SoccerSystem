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
            TournamentEntity tournament = _mapper.Map<TournamentEntity>(request.Tournament);

            if (await _tournamentRepository.GetTournamentByNameAsync(tournament.Name) != null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"The {tournament.Name} tournament name is already registered", State = State.error };

            if(!await _tournamentRepository.AddTournamentAsync(tournament))
                return new ActionResponse { IsSuccess = false, Title = "Error!", Message = $"Something has gone wrong", State = State.error };

            return new ActionResponse { IsSuccess = true, Title = "Created", Message = $"The tournament {tournament.Name} was created", State = State.success };
        }
    }
}
