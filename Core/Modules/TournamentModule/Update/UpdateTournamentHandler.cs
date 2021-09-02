using MediatR;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.Update
{
    public class UpdateTournamentHandler : IRequestHandler<UpdateTournamentCommnad, ActionResponse>
    {
        private readonly ITournamentRepository _tournamentRepository;

        public UpdateTournamentHandler(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<ActionResponse> Handle(UpdateTournamentCommnad request, CancellationToken cancellationToken)
        {
            TournamentResponse upTournament = request.TournamentResponse;

            TournamentEntity tournament = await _tournamentRepository.GetTournamentFindAsync(request.TournamentResponse.Id);
            if (tournament == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The tournament does not exist", State = State.error };

            TournamentEntity tournamentByName = await _tournamentRepository.GetTournamentByNameAsync(request.TournamentResponse.Name);
            if (tournamentByName != null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"The {tournamentByName.Name} tournament name is already registered", State = State.error };

            tournament.EndDate = upTournament.EndDate;
            tournament.IsActive = upTournament.IsActive;
            tournament.StartDate = upTournament.StartDate;
            tournament.Name = upTournament.Name ?? tournament.Name;
            tournament.LogoPath = upTournament.LogoPath ?? tournament.LogoPath;

            bool update = await _tournamentRepository.UpdateTournamentAsync(tournament);
            if(!update)
                return new ActionResponse { IsSuccess = true, Title = "Error!", Message = "Something has gone wrong", State = State.error };

            return new ActionResponse { IsSuccess = true, Title = "Updated!", Message = $"The tournament {tournament.Name} was Updated", State = State.success };
        }
    }
}
