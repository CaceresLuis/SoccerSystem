using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.Get
{
    public class GetGroupHandler : IRequestHandler<GetGroupQuery, GroupDto>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GetGroupHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<GroupDto> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            GroupEntity group = await _groupRepository.GetGroupWithTournamentAsync(request.Id);

            return _mapper.Map<GroupDto>(group);
        }
    }
}
