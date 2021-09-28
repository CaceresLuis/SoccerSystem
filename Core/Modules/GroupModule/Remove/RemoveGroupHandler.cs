using MediatR;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.Remove
{
    public class RemoveGroupHandler : IRequestHandler<RemoveGroupCommand, bool>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTeamsRepository _groupDetailsRepository;
        public RemoveGroupHandler(IGroupRepository groupRepository, IGroupTeamsRepository groupDetailsRepository)
        {
            _groupRepository = groupRepository;
            _groupDetailsRepository = groupDetailsRepository;
        }

        public async Task<bool> Handle(RemoveGroupCommand request, CancellationToken cancellationToken)
        {
            Infrastructure.Models.GroupEntity group = await _groupRepository.FindGroupByIdAsync(request.Id);
            if(group == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The group does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if (await _groupDetailsRepository.GetGroupDetailsByGroupAsync(group.Id) != null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The group has resgistered teams",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if(!await _groupRepository.DeleteGroupAsync(group))
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "Something has gone wrong",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            return true;
        }
    }
}
