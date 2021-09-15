﻿using MediatR;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Core.Modules.MatchModule.Close
{
    public class CloseMatchHandler : IRequestHandler<CloseMatchCommand, ActionResponse>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IGroupTeamsRepository _groupTeamsRepository;

        public CloseMatchHandler(IMatchRepository matchRepository, IGroupTeamsRepository groupTeamsRepository)
        {
            _matchRepository = matchRepository;
            _groupTeamsRepository = groupTeamsRepository;
        }

        public async Task<ActionResponse> Handle(CloseMatchCommand request, CancellationToken cancellationToken)
        {
            Dtos.MatchDto dto = request.MatchDto;
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
            return new ActionResponse { IsSuccess = true, Title = "Success", Message = "Macht Updated", State = State.success };
        }
    }
}