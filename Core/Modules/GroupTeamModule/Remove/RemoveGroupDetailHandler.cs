using MediatR;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System;

namespace Core.Modules.GroupTeamModule.Remove
{
    public class RemoveGroupDetailHandler : IRequestHandler<RemoveGroupDetailCommand, Guid>
    {
        private readonly IGroupTeamsRepository _groupDetailsRepository;

        public RemoveGroupDetailHandler(IGroupTeamsRepository groupDetailsRepository)
        {
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<Guid> Handle(RemoveGroupDetailCommand request, CancellationToken cancellationToken)
        {
            GroupTeamEntity groupDetailEntity = await _groupDetailsRepository.GetGroupDetailsAsync(request.Id);
            if (groupDetailEntity == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Not found",
                        Message = "The GroupTeam does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if (groupDetailEntity.MatchesPlayed > 0)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = $"{groupDetailEntity.Group.Id}",
                        Message = "The team does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if (!await _groupDetailsRepository.DeleteGroupDetailsAsync(groupDetailEntity))
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = $"{groupDetailEntity.Group.Id}",
                        Message = "Something has gone wrong",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });
            
            return groupDetailEntity.Group.Id; 
        }
    }
}
