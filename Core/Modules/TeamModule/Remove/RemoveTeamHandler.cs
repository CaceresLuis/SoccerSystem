using MediatR;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Shared.Helpers.Image;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TeamModule.Remove
{
    public class RemoveTeamHandler : IRequestHandler<RemoveTeamCommand, bool>
    {
        private readonly IIMageHelper _iMageHelper;
        private readonly ITeamRepository _teamRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IGroupTeamsRepository _groupDetailsRepository;

        public RemoveTeamHandler(ITeamRepository teamRepository, IGroupTeamsRepository groupDetailsRepository, IIMageHelper iMageHelper, IImageRepository imageRepository)
        {
            _iMageHelper = iMageHelper;
            _teamRepository = teamRepository;
            _imageRepository = imageRepository;
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<bool> Handle(RemoveTeamCommand request, CancellationToken cancellationToken)
        {
            TeamEntity team = await _teamRepository.FindTeamByIdAsync(request.IdTeam);
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

            if (await _groupDetailsRepository.GetGroupDetailsByTeamAsync(team.Id) != null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The team is currently in a tournament",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });
            bool deleteTeam = await _teamRepository.DeleteTeamAsync(team);
            if (!deleteTeam)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "Something has gone wrong",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });
            ImageEntity img = await _imageRepository.GetImage(team.Id);
            await _imageRepository.DeleteImage(img);
            _iMageHelper.DeleteImage(img.Path);

            return true;
        }
    }
}
