using MediatR;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.Update
{
    public class UpdateGroupHandler : IRequestHandler<UpdateGroupCommand, ActionResponse>
    {
        private readonly IGroupRepository _groupRepository;

        public UpdateGroupHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<ActionResponse> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            GroupEntity group = await _groupRepository.GetGroupWithTournamentAsync(request.Group.Id);
            if (group == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"The group does not exist", State = State.error };

            if (request.Group.Name != group.Name)
            {
                if (await _groupRepository.GetGroupByNameAndTournamentAsync(group.Tournament.Id, request.Group.Name) != null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"The {request.Group.Name} is already registered in this tournament", State = State.error };
            }

            group.Name = request.Group.Name ?? group.Name;

            group.IsActive = request.Group.IsActive;

            if (!await _groupRepository.UpdateGroupAsync(group))
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"Something has gone wrong", State = State.error };

            return new ActionResponse { IsSuccess = true, Title = "Updated!", Message = $"The group {group.Name} was updated", State = State.success };
        }
    }
}
