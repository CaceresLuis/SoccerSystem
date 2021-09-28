using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.List
{
    public class ListGroupHandler : IRequestHandler<ListGroupQuery, GroupDto[]>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public ListGroupHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<GroupDto[]> Handle(ListGroupQuery request, CancellationToken cancellationToken)
        {
            GroupEntity[] groupEntities = await _groupRepository.GetListGroupWithTournamentAsync();

            return _mapper.Map<GroupDto[]>(groupEntities);
        }
    }
}
