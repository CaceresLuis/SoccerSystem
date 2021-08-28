using MediatR;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.List
{
    public class ListTeamsHandler : IRequestHandler<ListTeamsQuery, TeamEntity[]>
    {
        private readonly ITeamRepository _teamRepository;

        public ListTeamsHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<TeamEntity[]> Handle(ListTeamsQuery request, CancellationToken cancellationToken)
        {
            return await _teamRepository.GetAllTeamAsync();
        }
    }
}
