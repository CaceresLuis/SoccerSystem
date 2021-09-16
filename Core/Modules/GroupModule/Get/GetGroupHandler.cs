using MediatR;
using AutoMapper;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.Get
{
    public class GetGroupHandler : IRequestHandler<GetGroupQuery, GroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GetGroupHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<GroupResponse> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            GroupEntity group = await _groupRepository.GetGroupWithTournamentAsync(request.Id);

            return _mapper.Map<GroupResponse>(group);
        }
    }
}
