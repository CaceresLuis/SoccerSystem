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
        private readonly ITournamentRepository _tournamentRepository;

        public UpdateTournamentHandler(ITournamentRepository tournamentRepository, IIMageHelper iMageHelper)
        {
            _iMageHelper = iMageHelper;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<bool> Handle(UpdateTournamentCommnad request, CancellationToken cancellationToken)
        {
            TournamentDto upTournament = request.TournamentDto;
            if (upTournament.LogoFile != null)
                upTournament.LogoPath = await _iMageHelper.UploadImageAsync(upTournament.LogoFile, "Teams");

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
            tournament.LogoPath = upTournament.LogoPath ?? tournament.LogoPath;

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

            return true;
        }
    }
}
