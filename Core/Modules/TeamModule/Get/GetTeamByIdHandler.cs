using MediatR;
using Shared.ViewModel;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.Get
{
    public class GetTeamByIdHandler : IRequestHandler<GetTeamByIdQuery, TeamViewModel>
    {
        private readonly ITeamRepository _teamRepository;

        public GetTeamByIdHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<TeamViewModel> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            Infrastructure.Models.TeamEntity team = await _teamRepository.FindTeamByIdAsync(request.TeamId);

            return new TeamViewModel { LogoPath = team.LogoPath, Name = team.Name };
        }
    }
}
