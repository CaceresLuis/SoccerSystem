using MediatR;
using Core.Dtos;
using System.Net;
using AutoMapper;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.Get
{
    public class FindTournamentHandler : IRequestHandler<FindTournamentQuery, TournamentDto>
    {
        private readonly IMapper _mapper;
        private readonly ITournamentRepository _tournamentRepository;

        public FindTournamentHandler(IMapper mapper, ITournamentRepository tournamentRepository)
        {
            _mapper = mapper;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<TournamentDto> Handle(FindTournamentQuery request, CancellationToken cancellationToken)
        {
            TournamentEntity tournament = await _tournamentRepository.GetTournamentDetailsAsync(request.Id);
            if (tournament == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The tournament does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            return _mapper.Map<TournamentDto>(tournament);
        }
    }
}
