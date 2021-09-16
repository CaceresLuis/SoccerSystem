using MediatR;
using AutoMapper;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Shared.Helpers.Image;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.Add
{
    public class AddTeamHandler : IRequestHandler<AddTeamCommand, ActionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IIMageHelper _iMageHelper;
        private readonly ITeamRepository _teamRepository;

        public AddTeamHandler(ITeamRepository teamRepository, IMapper mapper, IIMageHelper iMageHelper)
        {
            _mapper = mapper;
            _iMageHelper = iMageHelper;
            _teamRepository = teamRepository;
        }

        public async Task<ActionResponse> Handle(AddTeamCommand request, CancellationToken cancellationToken)
        {
            if (request.Team.LogoFile != null)
                request.Team.LogoPath = await _iMageHelper.UploadImageAsync(request.Team.LogoFile, "Tournaments");

            TeamEntity team = _mapper.Map<TeamEntity>(request.Team);

            if(await _teamRepository.FindTeamByNameAsync(team.Name) != null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"The {team.Name} is already registered", State = State.error };

            if(!await _teamRepository.AddTeamAsync(team))
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"Something has gone wrong", State = State.error }; ;

            return new ActionResponse { IsSuccess = true, Title = "Created", Message = $"The team {team.Name} was created", State = State.success };
        }
    }
}
