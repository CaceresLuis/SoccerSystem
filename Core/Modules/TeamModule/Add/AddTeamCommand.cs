using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TeamModule.Add
{
    public class AddTeamCommand : IRequest<ActionResponse>
    {
        public Team Team { get; set; }
    }
}
