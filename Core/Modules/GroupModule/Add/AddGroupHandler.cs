using MediatR;
using AutoMapper;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.Add
{
    public class AddGroupHandler : IRequestHandler<AddGroupCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public AddGroupHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<bool> Handle(AddGroupCommand request, CancellationToken cancellationToken)
        {
            GroupEntity group = _mapper.Map<GroupEntity>(request.Group);
            if(group.Name == null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The role name is emty",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            if (await _groupRepository.GetGroupByNameAndTournamentAsync(group.Tournament.Id, group.Name) != null)
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = $"The {group.Name} is already registered in this tournament",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });

            group.IsActive = true;
            if(!await _groupRepository.AddGroupAsync(group))
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
