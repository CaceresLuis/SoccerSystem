using MediatR;
using AutoMapper;
using System.Threading;
using Core.ModelResponse;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Core.Modules.GroupModule.Get
{
    public class GetGroupWithTournamentHandler : IRequestHandler<GetGroupWithTournamentQuery, Group>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GetGroupWithTournamentHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<Group> Handle(GetGroupWithTournamentQuery request, CancellationToken cancellationToken)
        {
            GroupEntity group = await _groupRepository.GetGroupWithTournamentAsync(request.Id);

            return _mapper.Map<Group>(group);
        }
    }
}
