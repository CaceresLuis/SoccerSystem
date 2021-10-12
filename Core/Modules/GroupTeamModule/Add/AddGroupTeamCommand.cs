using MediatR;
using Core.Dtos;
using Core.Dtos.AddDtos;

namespace Core.Modules.GroupTeamModule.Add
{
    public class AddGroupTeamCommand : IRequest<bool>
    {
        public AddGroupTeam AddGroupTeam { get; set; }
    }
}
