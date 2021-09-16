using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.MatchModule.List
{
    public class ListMatchByGroupHandler : IRequestHandler<ListMatchByGroupQuery, GroupMatchsDto>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public ListMatchByGroupHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<GroupMatchsDto> Handle(ListMatchByGroupQuery request, CancellationToken cancellationToken)
        {
            GroupEntity groupEntity = await _groupRepository.GetGroupMatchsAsync(request.GroupId);
            GroupMatchsDto groupMatchsDto = _mapper.Map<GroupMatchsDto>(groupEntity);
            return groupMatchsDto;
        }
    }
}
