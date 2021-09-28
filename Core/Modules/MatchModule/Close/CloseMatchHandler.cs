using MediatR;
using Core.Dtos;
using Core.Helpers;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System;

namespace Core.Modules.MatchModule.Close
{
    public class CloseMatchHandler : IRequestHandler<CloseMatchCommand, bool>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IResetMatchHelper _resetMatchHelper;
        private readonly IGroupTeamsRepository _groupTeamsRepository;

        public CloseMatchHandler(IMatchRepository matchRepository, IGroupTeamsRepository groupTeamsRepository, IResetMatchHelper resetMatchHelper)
        {
            _matchRepository = matchRepository;
            _resetMatchHelper = resetMatchHelper;
            _groupTeamsRepository = groupTeamsRepository;
        }

        public async Task<bool> Handle(CloseMatchCommand request, CancellationToken cancellationToken)
        {
            MatchDto dto = request.MatchDto;
            if (dto.Visitor != null && dto.Local != null)
            {
                dto.VisitorId = dto.Visitor.Id;
                dto.LocalId = dto.Local.Id;
            }

            MatchEntity match = await _matchRepository.FindMatchByIdAsync(dto.Id);
            if (match.IsClosed)
                await _resetMatchHelper.ResetMatchAsync(dto);

            GroupTeamEntity local = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(dto.GroupId, dto.LocalId);
            GroupTeamEntity visitor = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(dto.GroupId, dto.VisitorId);

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
