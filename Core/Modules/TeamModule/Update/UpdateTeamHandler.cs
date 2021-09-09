using MediatR;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.Update
{
    public class UpdateTeamHandler : IRequestHandler<UpdateTeamCommand, ActionResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public UpdateTeamHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<ActionResponse> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            TeamResponse upTeam = request.Team;

            TeamEntity team = await _teamRepository.FindTeamByIdAsync(upTeam.Id);
            if(team == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"The team does not exist", State = State.error };

            if (upTeam.Name != team.Name)
            {
                if(await _teamRepository.FindTeamByNameAsync(upTeam.Name) != null)
                    return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"The {upTeam.Name} team name is already registered", State = State.error };
            }

            team.Name = upTeam.Name;
            team.LogoPath = upTeam.LogoPath;

            if(!await _teamRepository.UpdateTeamAsync(team))
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"Something has gone wrong", State = State.error };
            
            return new ActionResponse { IsSuccess = true, Title = "Updated!", Message = $"The {request.Team.Name} team was Updated", State = State.success };
        }
    }
}
