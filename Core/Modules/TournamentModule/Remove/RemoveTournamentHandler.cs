using Core.ModelResponse;
using Infrastructure.Interfaces;
using MediatR;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            var exist = await _tournamentRepository.GetTournamentWithGroupAsync(request.Id);
            if(exist == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The tournament does not exist", State = State.error };

            if(exist.Groups.Count() > 0)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = $"The tournament {exist.Name} has registered groups", State = State.error };

            if (!await _tournamentRepository.DeleteTournamentAsync(exist))
                return new ActionResponse { IsSuccess = false, Message = "Something has gone wrong", State = State.error };

            return new ActionResponse {IsSuccess = true, Title = "Deleted!", Message = $"Tournament {exist.Name} has been deleted!", State = State.success };
        }
    }
}
