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
                return new ActionResponse { IsSuccess = false, Message = "The tournament don't exist", State = State.Failed };

            if(exist.Groups.Count() > 0)
                return new ActionResponse { IsSuccess = false, Message = "The tournament has registered groups", State = State.Failed };

            if (!await _tournamentRepository.DeleteTournamentAsync(exist))
                return new ActionResponse { IsSuccess = false, Message = "Something has goes wrong", State = State.Failed };

            return new ActionResponse {IsSuccess = true, Message = "Tournament Deleted!", State = State.Deleted };
        }
    }
}
