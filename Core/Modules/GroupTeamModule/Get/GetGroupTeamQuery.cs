using System;
using MediatR;
using Core.Dtos;

namespace Core.Modules.GroupTeamModule.Get
{
    public class GetGroupTeamQuery : IRequest<GroupTeamDto>
    {
        public Guid Id { get; set; }
    }
}
