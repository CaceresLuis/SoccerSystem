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
        private readonly IGroupTeamsRepository _groupDetailsRepository;

        public AddGroupTeamHandler(IGroupTeamsRepository groupDetailsRepository, ITeamRepository teamRepository = null, IGroupRepository groupRepository = null)
        {
            _teamRepository = teamRepository;
            _groupRepository = groupRepository;
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<bool> Handle(AddGroupTeamCommand request, CancellationToken cancellationToken)
        {
            var data = request.AddGroupTeamDto;
            if(data.Group != null)
                data.IdGroup = data.Group.Id;

            TeamEntity team = await _teamRepository.FindTeamByIdAsync(data.TeamId);
            GroupEntity group = await _groupRepository.FindGroupByIdAsync(data.IdGroup);
            if(team == null || group == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Not registered",
                        Message = "Something has gone wrong",
                        Title = "Not registered",
                        State = State.error,
                        IsSuccess = false
                    });

            GroupTeamEntity groupDetail = new GroupTeamEntity { Group = group, Team = team };

            if (!await _groupDetailsRepository.AddGroupDetailsAsync(groupDetail))
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
