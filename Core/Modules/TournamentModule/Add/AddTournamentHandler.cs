using MediatR;
using System.Threading;
using Shared.ViewModel;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.TournamentModule.Add
{
    public class AddTournamentHandler : IRequestHandler<AddTournamentCommand, bool>
    {
        private readonly ITournamentRepository _tournamentRepository;

        public AddTournamentHandler(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<bool> Handle(AddTournamentCommand request, CancellationToken cancellationToken)
        {
            TournamentViewModel<GroupEntity> t = request.Tournament;
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

            return await _tournamentRepository.AddTournamentAsync(tournament);
        }
    }
}
