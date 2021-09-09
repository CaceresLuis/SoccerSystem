using MediatR;
using AutoMapper;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using Core.ModelResponse.One;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.Get
{
    public class GetTeamByIdHandler : IRequestHandler<GetTeamByIdQuery, ATeamResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;

        public GetTeamByIdHandler(ITeamRepository teamRepository, IMapper mapper)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<ATeamResponse> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            ATeamResponse response = new ATeamResponse { };
            TeamEntity team = await _teamRepository.FindTeamByIdAsync(request.TeamId);
            if (team == null)
            {
                response.Data = new ActionResponse { IsSuccess = false, Title = "Error", Message = "The team does not exist", State = State.error };
                return response;
            }

            response.Team = _mapper.Map<TeamResponse>(team);

            return response;
        }
    }
}
