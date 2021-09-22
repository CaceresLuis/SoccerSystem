using MediatR;
using Core.Dtos;

namespace Core.Modules.TeamModule.Update
{
    public class UpdateTeamCommand : IRequest<bool>
    {
        public TeamDto Team { get; set; }
    }
}
