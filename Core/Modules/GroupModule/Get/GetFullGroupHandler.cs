using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Linq;

namespace Core.Modules.GroupModule.Get
{
    public class GetFullGroupHandler : IRequestHandler<GetFullGroupQuery, GroupFullData>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly IImageRepository _imageRepository;

        public GetFullGroupHandler(IMapper mapper, IGroupRepository groupRepository, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _imageRepository = imageRepository;
        }

        public async Task<GroupFullData> Handle(GetFullGroupQuery request, CancellationToken cancellationToken)
        {
            GroupEntity group = await _groupRepository.GetGroupTeamAndDetailsAsync(request.Id);
            if (group == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The group does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            GroupFullData groupDto = _mapper.Map<GroupFullData>(group);
            ImageEntity tournamentImg = await _imageRepository.GetImage(group.Tournament.Id);
            if (tournamentImg != null)
                groupDto.Tournament.LogoPath = tournamentImg.Path;

            foreach (TeamEntity team in group.GroupTeams.Select(a => a.Team))
            {
                ImageEntity img = await _imageRepository.GetImage(team.Id);
                foreach (TeamDto dto in groupDto.GroupTeams.Select(a => a.Team))
                {
                    if (team.Id == dto.Id)
                        dto.LogoPath = img.Path;
                }
            }

            return groupDto;
        }
    }
}
