using MediatR;

namespace Core.Modules.TeamModule.Remove
{
    public class RemoveTeamCommand : IRequest<bool>
    {
        public int IdTeam { get; set; }
    }
}
