using MediatR;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.Remove
{
    public class RemoveGroupHandler : IRequestHandler<RemoveGroupCommand, ActionResponse>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupDetailsRepository _groupDetailsRepository;
        public RemoveGroupHandler(IGroupRepository groupRepository, IGroupDetailsRepository groupDetailsRepository)
        {
            _groupRepository = groupRepository;
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<ActionResponse> Handle(RemoveGroupCommand request, CancellationToken cancellationToken)
        {
            Infrastructure.Models.GroupEntity group = await _groupRepository.FindGroupByIdAsync(request.Id);
            if(group == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The Group does not exist", State = State.error };

            if (await _groupDetailsRepository.GetGroupDetailsAsync(group.Id) != null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The group has resgistered teams", State = State.error };

            if(!await _groupRepository.DeleteGroupAsync(group))
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "Something has gone wrong", State = State.error };

            return new ActionResponse { IsSuccess = true, Title = "Deleted!", Message = $"Tournament {group.Name} has been deleted!", State = State.success };
        }
    }
}
