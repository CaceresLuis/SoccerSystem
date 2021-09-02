using System;
using MediatR;
using AutoMapper;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.Update
{
    public class UpdateTournamentHandler : IRequestHandler<UpdateTournamentCommnad, bool>
    {
        private readonly IMapper _mapper;
        private readonly ITournamentRepository _tournamentRepository;

        public UpdateTournamentHandler(IMapper mapper, ITournamentRepository tournamentRepository)
        {
            _mapper = mapper;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<bool> Handle(UpdateTournamentCommnad request, CancellationToken cancellationToken)
        {
            var upTournament = request.TournamentResponse;
            var tournament = await _tournamentRepository.GetTournamentFindAsync(request.TournamentResponse.Id);
            if (tournament == null)
                throw new Exception("El torneo no existe");

            tournament.EndDate = upTournament.EndDate;
            tournament.IsActive = upTournament.IsActive;
            tournament.StartDate = upTournament.StartDate;
            tournament.Name = upTournament.Name ?? tournament.Name;
            tournament.LogoPath = upTournament.LogoPath ?? tournament.LogoPath;

            return await _tournamentRepository.UpdateTournamentAsync(tournament);
        }
    }
}
