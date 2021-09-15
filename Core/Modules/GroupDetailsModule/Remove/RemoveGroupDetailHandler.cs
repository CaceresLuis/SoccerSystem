using MediatR;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using Core.ModelResponse.One;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupDetailsModule.Remove
{
    public class RemoveGroupDetailHandler : IRequestHandler<RemoveGroupDetailCommand, RGroupDetailsResponse>
    {
        private readonly IGroupTeamsRepository _groupDetailsRepository;

        public RemoveGroupDetailHandler(IGroupTeamsRepository groupDetailsRepository)
        {
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<RGroupDetailsResponse> Handle(RemoveGroupDetailCommand request, CancellationToken cancellationToken)
        {
            GroupTeamEntity groupDetailEntity = await _groupDetailsRepository.GetGroupDetailsAsync(request.Id);

            RGroupDetailsResponse response = new RGroupDetailsResponse {  };
            if (groupDetailEntity == null)
                response.Data = new ActionResponse { IsSuccess = false, Title = "Error", Message = "The team does not exist", State = State.error };


            if (!await _groupDetailsRepository.DeleteGroupDetailsAsync(groupDetailEntity))
                response.Data = new ActionResponse { IsSuccess = false, Title = "Error", Message = "Something has gone wrong", State = State.error };

            response.Data = new ActionResponse { IsSuccess = false, Title = "Deleted!", Message = $"Team {groupDetailEntity.Team.Name} has been deleted!", State = State.success };
            response.GroupId = groupDetailEntity.Group.Id;
            return response; 
        }
    }
}
