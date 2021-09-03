using MediatR;
using System.Linq;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.Remove
{
    public class RemoveTournamentHandler : IRequestHandler<RemoveTournamentCommand, ActionResponse>
    {
        private readonly ITournamentRepository _tournamentRepository;

        public RemoveTournamentHandler(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<ActionResponse> Handle(RemoveTournamentCommand request, CancellationToken cancellationToken)
        {
            Infrastructure.Models.TournamentEntity tournamnet = await _tournamentRepository.GetTournamentWithGroupAsync(request.Id);
            if(tournamnet == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The tournament does not exist", State = State.error };

            if(tournamnet.Groups.Count() > 0)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"The tournament {tournamnet.Name} has registered groups", State = State.error };

            if (!await _tournamentRepository.DeleteTournamentAsync(tournamnet))
                return new ActionResponse { IsSuccess = false, Message = "Something has gone wrong", State = State.error };

            return new ActionResponse {IsSuccess = true, Title = "Deleted!", Message = $"Tournament {tournamnet.Name} has been deleted!", State = State.success };
        }
    }
}
