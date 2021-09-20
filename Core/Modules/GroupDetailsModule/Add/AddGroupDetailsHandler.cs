using MediatR;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupDetailsModule.Add
{
    public class AddGroupDetailsHandler : IRequestHandler<AddGroupDetailsCommand, bool>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTeamsRepository _groupDetailsRepository;

        public AddGroupDetailsHandler(IGroupTeamsRepository groupDetailsRepository, ITeamRepository teamRepository = null, IGroupRepository groupRepository = null)
        {
            _teamRepository = teamRepository;
            _groupRepository = groupRepository;
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<bool> Handle(AddGroupDetailsCommand request, CancellationToken cancellationToken)
        {
            TeamEntity team = await _teamRepository.FindTeamByIdAsync(request.GroupDetail.TeamId);
            GroupEntity group = await _groupRepository.FindGroupByIdAsync(request.GroupDetail.Group.Id);
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
