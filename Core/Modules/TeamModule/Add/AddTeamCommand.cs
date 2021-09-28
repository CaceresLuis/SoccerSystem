using MediatR;
using Core.Dtos;

namespace Core.Modules.TeamModule.Add
{
    public class AddTeamCommand : IRequest<bool>
    {
        public TeamDto Team { get; set; }
    }
}
