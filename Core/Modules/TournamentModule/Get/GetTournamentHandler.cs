using MediatR;
using AutoMapper;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Core.ModelResponse.One;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.Get
{
    public class GetTournamentHandler : IRequestHandler<GetTournamentQuery, ATournamentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITournamentRepository _tournamentRepository;

        public GetTournamentHandler(ITournamentRepository tournamentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<ATournamentResponse> Handle(GetTournamentQuery request, CancellationToken cancellationToken)
        {
            ATournamentResponse response = new ATournamentResponse { Data = new ActionResponse { IsSuccess = true } };

            TournamentEntity tournament = await _tournamentRepository.GetTournamentDetailsAsync(request.Id);
            if(tournament == null)
            {
                response.Data = new ActionResponse { IsSuccess = false, Title = "Error", Message = "The tournament does not exist", State = State.error };
                return response;
            }

            response.Tournament = _mapper.Map<TournamentResponse>(tournament);
            return response;
        }
    }
}
