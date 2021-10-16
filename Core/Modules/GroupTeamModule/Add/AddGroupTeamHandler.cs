using MediatR;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupTeamModule.Add
{
    public class AddGroupTeamHandler : IRequestHandler<AddGroupTeamCommand, bool>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTeamsRepository _groupTeamsRepository;

        public AddGroupTeamHandler(IGroupTeamsRepository groupTeamsRepository, ITeamRepository teamRepository = null, IGroupRepository groupRepository = null)
        {
            _teamRepository = teamRepository;
            _groupRepository = groupRepository;
            _groupTeamsRepository = groupTeamsRepository;
        }

        public async Task<bool> Handle(AddGroupTeamCommand request, CancellationToken cancellationToken)
        {
            var data = request.AddGroupTeam;

            TeamEntity team = await _teamRepository.FindTeamByIdAsync(data.IdTeam);
            GroupEntity group = await _groupRepository.FindGroupByIdAsync(data.IdGroup);
            if(team == null || group == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Not registered",
                        Message = "Team or Group does't exits",
                        Title = "Not registered",
                        State = State.error,
                        IsSuccess = false
                    });

            GroupTeamEntity groupDetail = new GroupTeamEntity { Group = group, Team = team };
            var exist = await _groupTeamsRepository.GetGroupDetailsByGroupAdnTeamAsync(group.Id, team.Id);
            if(exist != null)
            throw new ExceptionHandler(HttpStatusCode.BadRequest,
                   new Error
                   {
                       Code = "Not registered",
                       Message = "This team already exists in the group",
                       Title = "Not registered",
                       State = State.error,
                       IsSuccess = false
                   });

            if (!await _groupTeamsRepository.AddGroupDetailsAsync(groupDetail))
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                   new Error
                   {
                       Code = "Not registered",
                       Message = "Something has gone wrong",
                       Title = "Not registered",
                       State = State.error,
                       IsSuccess = false
                   });

            return true;
        }
    }
}
