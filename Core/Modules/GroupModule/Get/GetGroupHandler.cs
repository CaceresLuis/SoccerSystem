using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Net;
using System.Linq;
using Shared.Enums;
using System.Threading;
using Core.Dtos.DtosApi;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.Get
{
    public class GetGroupHandler : IRequestHandler<GetGroupQuery, GroupFullDataApi>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly IImageRepository _imageRepository;

        public GetGroupHandler(IGroupRepository groupRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _imageRepository = imageRepository;
        }

        public async Task<GroupFullDataApi> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            GroupEntity group = await _groupRepository.GetFullGroupAsync(request.Id) ??
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The Group does't exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            GroupFullDataApi groupDto = _mapper.Map<GroupFullDataApi>(group);
            foreach (TeamEntity team in group.GroupTeams.Select(a => a.Team))
            {
                ImageEntity img = await _imageRepository.GetImage(team.Id);
                foreach (TeamDto dto in groupDto.GroupTeams.Select(a => a.Team))
                {
                    if (team.Id == dto.Id)
                        dto.LogoPath = img.Path;
                }
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
