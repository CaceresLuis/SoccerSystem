using MediatR;
using Infrastructure.Models;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.List
{
    public class ListTournamentsHandler : IRequestHandler<ListTournamentsQuery, TournamentEntity[]>
    {
        private readonly ITournamentRepository _tournamentRepository;

        public ListTournamentsHandler(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<TournamentEntity[]> Handle(ListTournamentsQuery request, CancellationToken cancellationToken)
        {
            return await _tournamentRepository.GetTournamentsDetailsAsync();
        }
    }
}
