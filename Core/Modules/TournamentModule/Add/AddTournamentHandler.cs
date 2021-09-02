﻿using MediatR;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.Add
{
    public class AddTournamentHandler : IRequestHandler<AddTournamentCommand, ActionResponse>
    {
        private readonly ITournamentRepository _tournamentRepository;

        public AddTournamentHandler(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<ActionResponse> Handle(AddTournamentCommand request, CancellationToken cancellationToken)
        {
            TournamentResponse t = request.Tournament;
            TournamentEntity tournament = new TournamentEntity 
            {
                IsActive = t.IsActive,
                EndDate = t.EndDate,
                Groups = t.Groups,
                Id = t.Id,
                LogoPath = "",  //TODO 
                Name = t.Name,
                StartDate = t.StartDate
            };

            var create = await _tournamentRepository.AddTournamentAsync(tournament);

            if(!create)
                return new ActionResponse { IsSuccess = false, Title = "Error!", Message = "The tournament {tournament.Name} was not created", State = State.error };

            return new ActionResponse { IsSuccess = true, Title = "Created", Message = $"The tournament {tournament.Name} was created", State = State.success };
        }
    }
}
