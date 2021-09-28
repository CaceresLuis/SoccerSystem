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

        public GetTeamByIdHandler(ITeamRepository teamRepository, IMapper mapper)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
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

            return _mapper.Map<TeamDto>(team); ;
        }
    }
}
