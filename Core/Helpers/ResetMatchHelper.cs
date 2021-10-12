using Core.Dtos.DtosApi;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

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

        public async Task<bool> ResetMatchAsync(CloseMatchDto closeMatchDto)
        {
            GroupTeamEntity local = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(closeMatchDto.GroupId, closeMatchDto.LocalId);
            GroupTeamEntity visitor = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(closeMatchDto.GroupId, closeMatchDto.VisitorId);
            MatchEntity match = await _matchRepository.FindMatchByIdAsync(closeMatchDto.IdMatch);

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
