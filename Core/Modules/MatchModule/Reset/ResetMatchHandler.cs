﻿using MediatR;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.MatchModule.Reset
{
    public class ResetMatchHandler : IRequestHandler<ResetMatchCommand, bool>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IGroupTeamsRepository _groupTeamsRepository;

        public ResetMatchHandler(IMatchRepository matchRepository, IGroupTeamsRepository groupTeamsRepository)
        {
            _matchRepository = matchRepository;
            _groupTeamsRepository = groupTeamsRepository;
        }

        public async Task<bool> Handle(ResetMatchCommand request, CancellationToken cancellationToken)
        {
            MatchEntity matchEntity = request.Match;
            GroupTeamEntity local = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(matchEntity.Id, matchEntity.Local.Id);
            GroupTeamEntity visitor = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(matchEntity.Id, matchEntity.Visitor.Id);
            MatchEntity match = await _matchRepository.FindMatchByIdAsync(matchEntity.Id);

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
