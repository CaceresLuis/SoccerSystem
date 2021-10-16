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

namespace Core.Modules.GroupTeamModule.Get
{
    public class GetGroupTeamHandler : IRequestHandler<GetGroupTeamQuery, GroupTeamDto>
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly IGroupTeamsRepository _groupTeamsRepository;

        public GetGroupTeamHandler(IMapper mapper, IImageRepository imageRepository, IGroupTeamsRepository groupTeamsRepository)
        {
            _mapper = mapper;
            _imageRepository = imageRepository;
            _groupTeamsRepository = groupTeamsRepository;
        }

        public async Task<GroupTeamDto> Handle(GetGroupTeamQuery request, CancellationToken cancellationToken)
        {
            GroupTeamEntity groupTeam = await _groupTeamsRepository.GetGroupDetailsAsync(request.Id);
            if (groupTeam == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The GroupTeam does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            GroupTeamDto groupTeamDto = _mapper.Map<GroupTeamDto>(groupTeam);
            ImageEntity img = await _imageRepository.GetImage(groupTeam.Team.Id);
            if (img != null)
                groupTeamDto.Team.LogoPath = img.Path;

            return groupTeamDto;
        }
    }
}
