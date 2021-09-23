using MediatR;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Core.ModelResponse.One;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupTeamModule.Update
{
    public class UpdateGroupDetailsHandler : IRequestHandler<UpdateGroupDetailsCommand, ActionResponse>
    {
        private readonly IGroupTeamsRepository _groupDetailsRepository;

        public UpdateGroupDetailsHandler(IGroupTeamsRepository groupDetailsRepository)
        {
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<ActionResponse> Handle(UpdateGroupDetailsCommand request, CancellationToken cancellationToken)
        {
            AGroupDetailResponse groupDetail = request.GroupDetail;
            GroupTeamEntity entity = await _groupDetailsRepository.GetGroupDetailsAsync(groupDetail.Id);
            if (entity == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The groupDetails does not exist", State = State.error };

            //Si (entity.GoalsFor != groupDetail.GoalsFor) respuesta es: groupDetail.GoalsFor sino respuesta es: entity.GoalsFor
            entity.GoalsFor = (entity.GoalsFor != groupDetail.GoalsFor) ? groupDetail.GoalsFor : entity.GoalsFor;
            entity.MatchesWon = (entity.MatchesWon != groupDetail.MatchesWon) ? groupDetail.MatchesWon : entity.MatchesWon;
            entity.MatchesLost = (entity.MatchesLost != groupDetail.MatchesLost) ? groupDetail.MatchesLost : entity.MatchesLost;
            entity.MatchesTied = (entity.MatchesTied != groupDetail.MatchesTied) ? groupDetail.MatchesTied : entity.MatchesTied;
            entity.GoalsAgainst = (entity.GoalsAgainst != groupDetail.GoalsAgainst) ? groupDetail.GoalsAgainst : entity.GoalsAgainst;

            entity.MatchesPlayed = entity.MatchesLost + entity.MatchesTied + entity.MatchesWon;

            if (!await _groupDetailsRepository.UpdateGroupDetailsAsync(entity))
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"Something has gone wrong", State = State.error };

            return new ActionResponse { IsSuccess = true, Title = "Updated", Message = $"Something has gone wrong", State = State.success };
        }
    }
}
