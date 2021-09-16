using MediatR;
using AutoMapper;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Core.ModelResponse.One;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.Get
{
    public class GetFullGroupHandler : IRequestHandler<GetFullGroupQuery, AGroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GetFullGroupHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<AGroupResponse> Handle(GetFullGroupQuery request, CancellationToken cancellationToken)
        {
            AGroupResponse response = new AGroupResponse { Data = new ActionResponse { IsSuccess = true } };

            GroupEntity group = await _groupRepository.GetGroupTeamAndDetailsAsync(request.Id);
            if (group == null)
            {
                response.Data = new ActionResponse { IsSuccess = false, Title = "Error", Message = "The group does not exist", State = State.error };
                return response;
            }

            response.Group = _mapper.Map<GroupResponse>(group);

            return response;
        }
    }
}
