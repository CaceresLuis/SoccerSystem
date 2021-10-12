using MediatR;
using AutoMapper;
using System.Threading;
using Core.Dtos.DtosApi;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.List
{
    public class ListGroupHandler : IRequestHandler<ListGroupQuery, GroupFullDataApi[]>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public ListGroupHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<GroupFullDataApi[]> Handle(ListGroupQuery request, CancellationToken cancellationToken)
        {
            GroupEntity[] groupEntities = await _groupRepository.GetListGroupWithTournamentAsync();

            return _mapper.Map<GroupFullDataApi[]>(groupEntities);
        }
    }
}
