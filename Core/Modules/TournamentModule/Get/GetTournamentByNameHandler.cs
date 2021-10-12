using AutoMapper;
using Core.Dtos;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.Get
{
    public class GetTournamentByNameHandler : IRequestHandler<GetTournamentByNameQuery, TournamentDto>
    {
        private readonly IMapper _mapper;
        private readonly ITournamentRepository _tournamentRepository;

        public GetTournamentByNameHandler(ITournamentRepository tournamentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<TournamentDto> Handle(GetTournamentByNameQuery request, CancellationToken cancellationToken)
        {
            Infrastructure.Models.TournamentEntity tournament = await _tournamentRepository.GetTournamentByNameAsync(request.Name);
            return _mapper.Map<TournamentDto>(tournament);
        }
    }
}
