using System;
using MediatR;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.Remove
{
    public class RemoveTeamHandler : IRequestHandler<RemoveTeamCommand, bool>
    {
        private readonly ITeamRepository _teamRepository;

        public RemoveTeamHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<bool> Handle(RemoveTeamCommand request, CancellationToken cancellationToken)
        {
            TeamEntity team = await _teamRepository.FindTeamByIdAsync(request.IdTeam);
            if ( team == null)
                throw new Exception("El team no existe");

            return await _teamRepository.DeleteTeamAsync(team);
        }
    }
}
