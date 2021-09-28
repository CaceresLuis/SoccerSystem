using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.Get
{
    public class GetTournamentHandler : IRequestHandler<GetTournamentQuery, TournamentFullData>
    {
        private readonly IMapper _mapper;
        private readonly ITournamentRepository _tournamentRepository;

        public GetTournamentHandler(ITournamentRepository tournamentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<TournamentFullData> Handle(GetTournamentQuery request, CancellationToken cancellationToken)
        {
            TournamentEntity tournament = await _tournamentRepository.GetTournamentDetailsAsync(request.Id);
            if(tournament == null)
                throw new ExceptionHandler(HttpStatusCode.NotFound,
                    new Error
                    {
                        Code = "Error",
                        Message = "The tournament does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            return _mapper.Map<TournamentFullData>(tournament);
        }
    }
}
