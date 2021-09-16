using MediatR;
using Core.Dtos;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.MatchModule.Reset
{
    public class ResetMatchHandler : IRequestHandler<ResetMatchCommand, ActionResponse>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IGroupTeamsRepository _groupTeamsRepository;

        public ResetMatchHandler(IMatchRepository matchRepository, IGroupTeamsRepository groupTeamsRepository)
        {
            _matchRepository = matchRepository;
            _groupTeamsRepository = groupTeamsRepository;
        }

        public async Task<ActionResponse> Handle(ResetMatchCommand request, CancellationToken cancellationToken)
        {
            MatchDto dto = request.MatchDto;
            GroupTeamEntity local = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(dto.GroupId, dto.Local.Id);
            GroupTeamEntity visitor = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(dto.GroupId, dto.Visitor.Id);
            MatchEntity match = await _matchRepository.FindMatchByIdAsync(dto.Id);

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
            return new ActionResponse { IsSuccess = true, Title = "Success", Message = "Macht Updated", State = State.success };
        }
    }
}
