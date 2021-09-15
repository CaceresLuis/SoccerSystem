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
        private readonly IGroupTeamsRepository _groupDetailsRepository;

        public RemoveTeamHandler(ITeamRepository teamRepository, IGroupTeamsRepository groupDetailsRepository)
        {
            _teamRepository = teamRepository;
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<ActionResponse> Handle(RemoveTeamCommand request, CancellationToken cancellationToken)
        {
            TeamEntity team = await _teamRepository.FindTeamByIdAsync(request.IdTeam);
            if (team == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The team does not exist", State = State.error };

            if(await _groupDetailsRepository.GetGroupDetailsByTeamAsync(team.Id) != null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The team is currently in a tournament", State = State.error };


            if (!await _teamRepository.DeleteTeamAsync(team))
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "Something has gone wrong", State = State.error };
                
            return new ActionResponse { IsSuccess = false, Title = "Deleted!", Message = $"Team {team.Name} has been deleted!", State = State.success };
        }
    }
}
