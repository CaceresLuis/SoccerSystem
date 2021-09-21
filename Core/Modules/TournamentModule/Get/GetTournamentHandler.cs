using MediatR;
using AutoMapper;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
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
            ATournamentResponse response = new ATournamentResponse { };

            TournamentEntity tournament = await _tournamentRepository.GetTournamentDetailsAsync(request.Id);
            if(tournament == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The tournament does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            response.Tournament = _mapper.Map<TournamentResponse>(tournament);
            return response;
        }
    }
}
