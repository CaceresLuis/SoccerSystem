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
        private readonly ITournamentRepository _tournamentRepository;

        public AddGroupHandler(IMapper mapper, IGroupRepository groupRepository, ITournamentRepository tournamentRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<bool> Handle(AddGroupCommand request, CancellationToken cancellationToken)
        {
            TournamentEntity tournamentEntity = await _tournamentRepository.GetTournamentFindAsync(request.GroupDto.Tournament.Id);
            GroupEntity group = _mapper.Map<GroupEntity>(request.GroupDto);
            group.IsActive = true;
            group.Tournament = tournamentEntity;
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

            if (!await _groupRepository.AddGroupAsync(group))
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
