using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.List
{
    public class ListTournamentsHandler : IRequestHandler<ListTournamentsQuery, TournamentFullData[]>
    {
        private readonly IMapper _mapper;
        private readonly ITournamentRepository _tournamentRepository;

        public ListTournamentsHandler(ITournamentRepository tournamentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<TournamentFullData[]> Handle(ListTournamentsQuery request, CancellationToken cancellationToken)
        {
            TournamentEntity[] tournament = await _tournamentRepository.GetTournamentsDetailsAsync();

            return _mapper.Map<TournamentFullData[]>(tournament);
        }
    }
}
