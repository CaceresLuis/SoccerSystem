using MediatR;
using Shared.ViewModel;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Core.ModelResponse.One;
using Infrastructure.Models;
using Core.ModelResponse;
using Shared.Enums;
using AutoMapper;

namespace Core.Modules.TeamModule.Get
{
    public class GetTeamByIdHandler : IRequestHandler<GetTeamByIdQuery, OneTeamResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;

        public GetTeamByIdHandler(ITeamRepository teamRepository, IMapper mapper)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
        }

        public async Task<OneTeamResponse> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            OneTeamResponse response = new OneTeamResponse { };
            TeamEntity team = await _teamRepository.FindTeamByIdAsync(request.TeamId);
            if (team == null)
            {
                response.Data = new ActionResponse { IsSuccess = false, Title = "Error", Message = "The team does not exist", State = State.error };
                return response;
            }

            response.Team = _mapper.Map<Team>(team);

            return response;
        }
    }
}
