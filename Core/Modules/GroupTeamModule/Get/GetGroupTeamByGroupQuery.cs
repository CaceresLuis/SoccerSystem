using MediatR;
using Core.Dtos;

namespace Core.Modules.GroupTeamModule.Get
{
    public class GetGroupTeamByGroupQuery : IRequest<AddGroupTeamDto>
    {
        public int IdGroup { get; set; }
        public int IdTournament { get; set; }
    }
}
