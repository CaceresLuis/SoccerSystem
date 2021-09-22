using MediatR;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Core.ModelResponse;
using Shared.Helpers.Image;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.Update
{
    public class UpdateTeamHandler : IRequestHandler<UpdateTeamCommand, bool>
    {
        private readonly IIMageHelper _iMageHelper;
        private readonly ITeamRepository _teamRepository;

        public UpdateTeamHandler(ITeamRepository teamRepository, IIMageHelper iMageHelper)
        {
            _iMageHelper = iMageHelper;
            _teamRepository = teamRepository;
        }

        public async Task<bool> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            TeamResponse upTeam = request.Team;
            if(upTeam.LogoFile != null)
                upTeam.LogoPath = await _iMageHelper.UploadImageAsync(upTeam.LogoFile, "Teams");

            TeamEntity team = await _teamRepository.FindTeamByIdAsync(upTeam.Id);
            if(team == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The team does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if (upTeam.Name != team.Name)
            {
                if(await _teamRepository.FindTeamByNameAsync(upTeam.Name) != null)
                    throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = $"The {upTeam.Name} team name is already registered",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });
            }

            team.Name = upTeam.Name;
            team.LogoPath = upTeam.LogoPath;

            if(!await _teamRepository.UpdateTeamAsync(team))
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "Something has gone wrong",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            return true;
        }
    }
}
