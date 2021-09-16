using MediatR;
using AutoMapper;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.Add
{
    public class AddGroupHandler : IRequestHandler<AddGroupCommand, ActionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public AddGroupHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<ActionResponse> Handle(AddGroupCommand request, CancellationToken cancellationToken)
        {
            GroupEntity group = _mapper.Map<GroupEntity>(request.Group);
            if(await _groupRepository.GetGroupByNameAndTournamentAsync(group.Tournament.Id, group.Name) != null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"The {group.Name} is already registered in this tournament", State = State.error };

            group.IsActive = true;
            if(!await _groupRepository.AddGroupAsync(group))
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"Something has gone wrong", State = State.error };

            return new ActionResponse { IsSuccess = true, Title = "Created!", Message = $"The group {group.Name} was created", State = State.success };
        }
    }
}
