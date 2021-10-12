using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Linq;
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
        private readonly IImageRepository _imageRepository;

        public ListMatchByGroupHandler(IMapper mapper, IGroupRepository groupRepository, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _imageRepository = imageRepository;
        }

        public async Task<GroupMatchsDto> Handle(ListMatchByGroupQuery request, CancellationToken cancellationToken)
        {
            GroupEntity groupEntity = await _groupRepository.GetFullGroupAsync(request.GroupId);
            GroupMatchsDto groupDto = _mapper.Map<GroupMatchsDto>(groupEntity);
            foreach (TeamEntity team in groupEntity.GroupTeams.Select(a => a.Team))
            {
                ImageEntity img = await _imageRepository.GetImage(team.Id);
                foreach (TeamDto dto in groupDto.Matches.Select(a => a.Local))
                {
                    if (team.Id == dto.Id)
                        dto.LogoPath = img.Path;
                }
                foreach (TeamDto dto in groupDto.Matches.Select(a => a.Visitor))
                {
                    if (team.Id == dto.Id)
                        dto.LogoPath = img.Path;
                }
            }

            return groupDto;
        }
    }
}
