using MediatR;
using Core.Dtos;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMatchRepository _matchRepository;

        public AddMatchHandler(IMapper mapper, IMatchRepository matchRepository, IGroupRepository groupRepository, ITeamRepository teamRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _matchRepository = matchRepository;
            _groupRepository = groupRepository;
        }

        public async Task<ActionResponse> Handle(AddMatchCommand request, CancellationToken cancellationToken)
        {
            MatchDto matchDto = request.MatchDto;
            MatchEntity matchEntity = _mapper.Map<MatchEntity>(matchDto);

            GroupEntity groupEntity = await _groupRepository.FindGroupByIdAsync(matchEntity.Group.Id);
            if(groupEntity == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The group does not exist", State = State.error };

            TeamEntity visitor = await _teamRepository.FindTeamByIdAsync(matchDto.Visitor.Id);
            TeamEntity local = await _teamRepository.FindTeamByIdAsync(matchDto.Local.Id);
            if(visitor == null && local == null)
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "The teams does not exist", State = State.error };
            
            if(!await _matchRepository.AddMatchAsync(matchEntity))
                return new ActionResponse { IsSuccess = false, Title = "Error", Message = "Sometime was wrong", State = State.error };
                
            
            return new ActionResponse { IsSuccess = true, Title = "Success", Message = "Macht added", State = State.success };
        }
    }
}
