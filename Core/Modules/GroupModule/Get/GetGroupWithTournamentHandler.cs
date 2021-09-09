using MediatR;
using AutoMapper;
using System.Threading;
using Core.ModelResponse;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Core.Modules.GroupModule.Get
{
    public class GetGroupWithTournamentHandler : IRequestHandler<GetGroupWithTournamentQuery, GroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GetGroupWithTournamentHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<GroupResponse> Handle(GetGroupWithTournamentQuery request, CancellationToken cancellationToken)
        {
            GroupEntity group = await _groupRepository.GetGroupWithTournamentAsync(request.Id);

            return _mapper.Map<GroupResponse>(group);
        }
    }
}
