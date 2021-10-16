using MediatR;
using Core.Dtos;
using System.Net;
using AutoMapper;
using Shared.Enums;
using System.Threading;
using Shared.Exceptions;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Core.Modules.GroupModule.Get
{
    public class GetSimpleGroupHandler : IRequestHandler<GetSimpleGroupQuery, GroupDto>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GetSimpleGroupHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task<GroupDto> Handle(GetSimpleGroupQuery request, CancellationToken cancellationToken)
        {
            GroupEntity group = await _groupRepository.GetFullGroupAsync(request.Id) ??
                throw new ExceptionHandler(HttpStatusCode.BadRequest,
                    new Error
                    {
                        Code = "Error",
                        Message = "The Group does't exist",
                        Title = "Error",
                        State = State.error,
                        IsSuccess = false
                    });


            return _mapper.Map<GroupDto>(group);
        }
    }
}
