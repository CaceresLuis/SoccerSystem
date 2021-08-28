using Infrastructure.Interfaces;
using Infrastructure.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            if (await _teamRepository.FindTeamByNameAsync(request.Name) != null)
                throw new Exception("Ese team ya existe");

            TeamEntity team = new TeamEntity { Name = request.Name };
            return await _teamRepository.UpdateTeamAsync(team);
        }
    }
}
