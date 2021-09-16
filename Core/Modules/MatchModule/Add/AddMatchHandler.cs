using MediatR;
using Core.Dtos;
using Shared.Enums;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.MatchModule.Add
{
    public class AddMatchHandler : IRequestHandler<AddMatchCommand, ActionResponse>
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

        public async Task<ActionResponse> Handle(AddMatchCommand request, CancellationToken cancellationToken)
        {
            AddMatchDto matchDto = request.AddMatchDto;

            GroupEntity groupEntity = await _groupRepository.FindGroupByIdAsync(matchDto.Group.Id);
            if(groupEntity == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The group does not exist", State = State.error };

            TeamEntity visitor = await _teamRepository.FindTeamByIdAsync(matchDto.VisitorId);
            TeamEntity local = await _teamRepository.FindTeamByIdAsync(matchDto.LocalId);
            if(visitor == null && local == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The teams does not exist", State = State.error };

            MatchEntity matchEntity = new MatchEntity { Group = groupEntity, Visitor = visitor, Local = local, Date = matchDto.Date, Hour = matchDto.Hour };
            
            if(!await _matchRepository.AddMatchAsync(matchEntity))
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "Sometime was wrong", State = State.error };
                
            
            return new ActionResponse { IsSuccess = true, Title = "Success", Message = "Macht added", State = State.success };
        }
    }
}
