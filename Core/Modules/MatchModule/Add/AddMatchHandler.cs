using MediatR;
using Core.Dtos;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.MatchModule.Add
{
    public class AddMatchHandler : IRequestHandler<AddMatchCommand, bool>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMatchRepository _matchRepository;

        public AddMatchHandler(IMatchRepository matchRepository, IGroupRepository groupRepository, ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
            _matchRepository = matchRepository;
            _groupRepository = groupRepository;
        }

        public async Task<bool> Handle(AddMatchCommand request, CancellationToken cancellationToken)
        {
            AddMatchDto matchDto = request.AddMatchDto;

            GroupEntity groupEntity = await _groupRepository.FindGroupByIdAsync(matchDto.Group.Id);
            if(groupEntity == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The group does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            TeamEntity visitor = await _teamRepository.FindTeamByIdAsync(matchDto.VisitorId);
            TeamEntity local = await _teamRepository.FindTeamByIdAsync(matchDto.LocalId);
            if(visitor == null && local == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The teams does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            MatchEntity matchEntity = new MatchEntity { Group = groupEntity, Visitor = visitor, Local = local, Date = matchDto.Date, Hour = matchDto.Hour };
            
            if(!await _matchRepository.AddMatchAsync(matchEntity))
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "Sometime was wrong",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });                
            
            return true;
        }
    }
}
