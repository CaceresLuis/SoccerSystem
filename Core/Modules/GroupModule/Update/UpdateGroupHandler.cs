using MediatR;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.Update
{
    public class UpdateGroupHandler : IRequestHandler<UpdateGroupCommand, bool>
    {
        private readonly IGroupRepository _groupRepository;

        public UpdateGroupHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<bool> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            GroupEntity group = await _groupRepository.GetGroupWithTournamentAsync(request.Group.Id);
            if (group == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The group does not exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if (request.Group.Name != group.Name)
            {
                if (await _groupRepository.GetGroupByNameAndTournamentAsync(group.Tournament.Id, request.Group.Name) != null)
                    throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = $"The {request.Group.Name} is already registered in this tournament",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });
            }

            group.Name = request.Group.Name ?? group.Name;

            group.IsActive = request.Group.IsActive;

            if (!await _groupRepository.UpdateGroupAsync(group))
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
