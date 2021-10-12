using System;
using MediatR;
using Core.Dtos;

namespace Core.Modules.GroupTeamModule.Get
{
    public class GetGroupTeamByGroupQuery : IRequest<AddGroupTeamDto>
    {
        public Guid IdGroup { get; set; }
        public Guid IdTournament { get; set; }
    }
}
