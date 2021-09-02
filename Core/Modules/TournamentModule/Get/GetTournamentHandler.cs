using AutoMapper;
using Core.ModelResponse;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Modules.TournamentModule.Get
{
    public class GetTournamentHandler : IRequestHandler<GetTournamentQuery, TournamentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITournamentRepository _tournamentRepository;

        public GetTournamentHandler(ITournamentRepository tournamentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<TournamentResponse> Handle(GetTournamentQuery request, CancellationToken cancellationToken)
        {
            var tournament = await _tournamentRepository.GetTournamentDetailsAsync(request.Id);
            return _mapper.Map<TournamentResponse>(tournament);
        }
    }
}
