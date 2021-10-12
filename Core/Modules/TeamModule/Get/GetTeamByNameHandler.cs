using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.Get
{
    public class GetTeamByNameHandler : IRequestHandler<GetTeamByNameQuery, TeamDto>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;

        public GetTeamByNameHandler(IMapper mapper, ITeamRepository teamRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<TeamDto> Handle(GetTeamByNameQuery request, CancellationToken cancellationToken)
        {
            Infrastructure.Models.TeamEntity team = await _teamRepository.FindTeamByNameAsync(request.TeamName);
            if ( team == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                new Error
                {
                    Code = "Error",
                    Message = $"The {request.TeamName} team name not exist",
                    Title = "Error",
                    State = State.error,
                    IsSuccess = false
                });

            return _mapper.Map<TeamDto>(team);
        }
    }
}
