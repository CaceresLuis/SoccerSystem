using MediatR;
using Core.Dtos;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.MatchModule.Close
{
    public class CloseMatchHandler : IRequestHandler<CloseMatchCommand, bool>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IGroupTeamsRepository _groupTeamsRepository;

        public CloseMatchHandler(IMatchRepository matchRepository, IGroupTeamsRepository groupTeamsRepository)
        {
            _matchRepository = matchRepository;
            _groupTeamsRepository = groupTeamsRepository;
        }

        public async Task<bool> Handle(CloseMatchCommand request, CancellationToken cancellationToken)
        {
            MatchDto dto = request.MatchDto;
            GroupTeamEntity local = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(dto.GroupId, dto.Local.Id);
            GroupTeamEntity visitor = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(dto.GroupId, dto.Visitor.Id);
            MatchEntity match = await _matchRepository.FindMatchByIdAsync(dto.Id);

            local.MatchesPlayed++;
            local.GoalsFor += dto.GoalsLocal;
            local.GoalsAgainst += dto.GoalsVisitor;

            visitor.MatchesPlayed++;
            visitor.GoalsFor += dto.GoalsVisitor;
            visitor.GoalsAgainst += dto.GoalsLocal;

            match.GoalsLocal = dto.GoalsLocal;
            match.GoalsVisitor = dto.GoalsVisitor;
            match.IsClosed = true;

            if (dto.GoalsLocal > dto.GoalsVisitor)
            {
                local.MatchesWon++;
                visitor.MatchesLost++;
            }
            else if (dto.GoalsVisitor > dto.GoalsLocal)
            {
                visitor.MatchesWon++;
                local.MatchesLost++;
            }
            else
            {
                local.MatchesTied++;
                visitor.MatchesTied++;
            }

            await _groupTeamsRepository.UpdateGroupDetailsAsync(local);
            await _groupTeamsRepository.UpdateGroupDetailsAsync(visitor);
            await _matchRepository.UpdateMatchAsync(match);
            return true;
        }
    }
}
