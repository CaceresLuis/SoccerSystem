using MediatR;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupDetailsModule.Add
{
    public class AddGroupDetailsHandler : IRequestHandler<AddGroupDetailsCommand, ActionResponse>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTeamsRepository _groupDetailsRepository;

        public AddGroupDetailsHandler(IGroupTeamsRepository groupDetailsRepository, ITeamRepository teamRepository = null, IGroupRepository groupRepository = null)
        {
            _teamRepository = teamRepository;
            _groupRepository = groupRepository;
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<ActionResponse> Handle(AddGroupDetailsCommand request, CancellationToken cancellationToken)
        {
            TeamEntity team = await _teamRepository.FindTeamByIdAsync(request.GroupDetail.TeamId);
            GroupEntity group = await _groupRepository.FindGroupByIdAsync(request.GroupDetail.Group.Id);
            if(team == null || group == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "Something has gone wrong", State = State.error };

            GroupTeamEntity groupDetail = new GroupTeamEntity { Group = group, Team = team };

            if (!await _groupDetailsRepository.AddGroupDetailsAsync(groupDetail))
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "Something has gone wrong", State = State.error }; ;

            return new ActionResponse { IsSuccess = true, Title = "Added", Message = "The team was added to the group", State = State.success };
        }
    }
}
