using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TeamModule.Update
{
    public class UpdateTeamCommand : IRequest<ActionResponse>
    {
        public Team Team { get; set; }
    }
}
