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

namespace Core.Modules.TournamentModule.Update
{
    public class UpdateTournamentHandler : IRequestHandler<UpdateTournamentCommnad, bool>
    {
        private readonly IIMageHelper _iMageHelper;
        private readonly IImageRepository _imageRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public UpdateTournamentHandler(ITournamentRepository tournamentRepository, IIMageHelper iMageHelper, IImageRepository imageRepository)
        {
            _iMageHelper = iMageHelper;
            _imageRepository = imageRepository;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<bool> Handle(UpdateTournamentCommnad request, CancellationToken cancellationToken)
        {
            TournamentDto upTournament = request.TournamentDto;
            TournamentEntity tournament = await _tournamentRepository.GetTournamentFindAsync(request.TournamentDto.Id);
            if (tournament == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The tournament does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if (upTournament.Name != tournament.Name)
            {
                if (await _tournamentRepository.GetTournamentByNameAsync(request.TournamentDto.Name) != null)
                    throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = $"The {upTournament.Name} tournament name is already registered",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });
            }

            tournament.EndDate = (upTournament.EndDate == null) ? tournament.EndDate : upTournament.EndDate;
            tournament.IsActive = upTournament.IsActive;
            tournament.StartDate = (upTournament.StartDate == null) ? tournament.StartDate : upTournament.StartDate;
            tournament.Name = upTournament.Name ?? tournament.Name;

            if(!await _tournamentRepository.UpdateTournamentAsync(tournament))
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "Something has gone wrong",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if (upTournament.LogoFile != null)
            {
                ImageEntity img = await _imageRepository.GetImage(upTournament.Id);
                _iMageHelper.DeleteImage(img.Path);
                string newImg = await _iMageHelper.UploadImageAsync(upTournament.LogoFile, "Tournaments");
                img.Path = newImg;
                await _imageRepository.UpdateImage(img);
            }

            return true;
        }
    }
}
