using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TeamModule.Update
{
    public class UpdateTeamCommand : IRequest<ActionResponse>
    {
        public TeamResponse Team { get; set; }
    }
}
