using MediatR;
using Core.Dtos;

namespace Core.Modules.GroupTeamModule.Add
{
    public class AddGroupTeamCommand : IRequest<bool>
    {
        public AddGroupTeamDto AddGroupTeamDto { get; set; }
    }
}
