using MediatR;
using AutoMapper;
using System.Net;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.Get
{
    public class GetFullGroupHandler : IRequestHandler<GetFullGroupQuery, GroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GetFullGroupHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<GroupResponse> Handle(GetFullGroupQuery request, CancellationToken cancellationToken)
        {
            GroupEntity group = await _groupRepository.GetGroupTeamAndDetailsAsync(request.Id);
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

            return _mapper.Map<GroupResponse>(group);
        }
    }
}
