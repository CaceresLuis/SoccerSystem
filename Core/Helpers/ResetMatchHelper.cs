using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Core.Dtos;

namespace Core.Helpers
{
    public class ResetMatchHelper : IResetMatchHelper
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IGroupTeamsRepository _groupTeamsRepository;

        public ResetMatchHelper(IMatchRepository matchRepository, IGroupTeamsRepository groupTeamsRepository)
        {
            _matchRepository = matchRepository;
            _groupTeamsRepository = groupTeamsRepository;
        }

        public async Task<bool> ResetMatchAsync(MatchDto matchDto)
        {
            if (matchDto.Visitor != null && matchDto.Local != null)
            {
                matchDto.VisitorId = matchDto.Visitor.Id;
                matchDto.LocalId = matchDto.Local.Id;
            }

            GroupTeamEntity local = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(matchDto.GroupId, matchDto.LocalId);
            GroupTeamEntity visitor = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(matchDto.GroupId, matchDto.VisitorId);
            MatchEntity match = await _matchRepository.FindMatchByIdAsync(matchDto.Id);

            local.MatchesPlayed--;
            local.GoalsFor -= match.GoalsLocal;
            local.GoalsAgainst -= match.GoalsVisitor;

            visitor.MatchesPlayed--;
            visitor.GoalsFor -= match.GoalsVisitor;
            visitor.GoalsAgainst -= match.GoalsLocal;

            if (match.GoalsLocal > match.GoalsVisitor)
            {
                local.MatchesWon--;
                visitor.MatchesLost--;
            }
            else if (match.GoalsVisitor > match.GoalsLocal)
            {
                visitor.MatchesWon--;
                local.MatchesLost--;
            }
            else
            {
                local.MatchesTied--;
                visitor.MatchesTied--;
            }

            await _groupTeamsRepository.UpdateGroupDetailsAsync(local);
            await _groupTeamsRepository.UpdateGroupDetailsAsync(visitor);

            match.GoalsVisitor = 0;
            match.GoalsLocal = 0;
            await _matchRepository.UpdateMatchAsync(match);

            return true;
        }
    }
}
