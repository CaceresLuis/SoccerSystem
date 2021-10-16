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

namespace Core.Modules.TeamModule.Get
{
    public class GetTeamByIdHandler : IRequestHandler<GetTeamByIdQuery, TeamDto>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly IImageRepository _imageRepository;

        public GetTeamByIdHandler(ITeamRepository teamRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _imageRepository = imageRepository;
        }

        public async Task<TeamDto> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            TeamEntity team = await _teamRepository.FindTeamByIdAsync(request.TeamId);
            if (team == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The team does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            TeamDto dto = _mapper.Map<TeamDto>(team);
            ImageEntity img = await _imageRepository.GetImage(team.Id);
            if(img != null)    
                dto.LogoPath = img.Path;

            return dto;
        }
    }
}
