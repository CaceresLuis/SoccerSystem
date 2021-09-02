using System;
using MediatR;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.Update
{
    public class UpdateTeamHandler : IRequestHandler<UpdateTeamCommand, bool>
    {
        private readonly ITeamRepository _teamRepository;

        public UpdateTeamHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<bool> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            if (await _teamRepository.FindTeamByNameAsync(request.TeamViewModel.Name) != null)
                throw new Exception("Ese team ya existe");

            TeamEntity team = new TeamEntity { Id = request.TeamViewModel.Id ,Name = request.TeamViewModel.Name };
            return await _teamRepository.UpdateTeamAsync(team);
        }
    }
}
