using MediatR;
using Core.Helpers;
using System.Threading;
using Core.Dtos.DtosApi;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Shared.Exceptions;
using System.Net;
using Shared.Enums;

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
            CloseMatchDto dto = request.CloseMatchDto;
            MatchEntity match = await _matchRepository.FindMatchByIdAsync(dto.IdMatch) ??
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The match does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });


            if (match.IsClosed)
                await _resetMatchHelper.ResetMatchAsync(dto);

            GroupTeamEntity local = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(match.Group.Id, dto.LocalId);
            GroupTeamEntity visitor = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(match.Group.Id, dto.VisitorId);

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
