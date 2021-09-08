using MediatR;
using AutoMapper;
using System.Threading;
using Core.ModelResponse;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.List
{
    public class ListTournamentsHandler : IRequestHandler<ListTournamentsQuery, Tournament[]>
    {
        private readonly IMapper _mapper;
        private readonly ITournamentRepository _tournamentRepository;

        public ListTournamentsHandler(ITournamentRepository tournamentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<Tournament[]> Handle(ListTournamentsQuery request, CancellationToken cancellationToken)
        {
            var tournament = await _tournamentRepository.GetTournamentsDetailsAsync();

            return _mapper.Map<Tournament[]>(tournament);
        }
    }
}
