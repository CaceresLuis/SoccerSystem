using MediatR;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.Remove
{
    public class RemoveTeamHandler : IRequestHandler<RemoveTeamCommand, ActionResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public RemoveTeamHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<ActionResponse> Handle(RemoveTeamCommand request, CancellationToken cancellationToken)
        {
            TeamEntity team = await _teamRepository.FindTeamByIdAsync(request.IdTeam);
            if (team == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The team does not exist", State = State.error };

            if(!await _teamRepository.DeleteTeamAsync(team))
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "Something has gone wrong", State = State.error };
                
            return new ActionResponse { IsSuccess = false, Title = "Deleted!", Message = $"Team {team.Name} has been deleted!", State = State.success };
        }
    }
}
