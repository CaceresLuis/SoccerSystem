using MediatR;
using Core.Dtos;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
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
        private readonly IImageRepository _imageRepository;

        public UpdateTeamHandler(ITeamRepository teamRepository, IIMageHelper iMageHelper, IImageRepository imageRepository)
        {
            _iMageHelper = iMageHelper;
            _teamRepository = teamRepository;
            _imageRepository = imageRepository;
        }

        public async Task<bool> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            TeamDto upTeam = request.Team;
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

            team.Name = upTeam.Name ?? team.Name;

            if (!await _teamRepository.UpdateTeamAsync(team))
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "Something has gone wrong",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            ImageEntity img = await _imageRepository.GetImage(upTeam.Id);
            if (upTeam.LogoFile != null && img != null)
            {
                _iMageHelper.DeleteImage(img.Path);
                string newImg = await _iMageHelper.UploadImageAsync(upTeam.LogoFile, "Teams");
                img.Path = newImg;
                await _imageRepository.UpdateImage(img);

                return true;
            }

            if(upTeam.LogoFile != null) 
            {
                string local = await _iMageHelper.UploadImageAsync(upTeam.LogoFile, "Teams");
                await _imageRepository.AddImage(local, team.Id);
            }

            return true;
        }
    }
}
