using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TeamModule.Remove
{
    public class RemoveTeamCommand : IRequest<bool>
    {
        public int IdTeam { get; set; }
    }
}
