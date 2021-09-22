using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TeamModule.Update
{
    public class UpdateTeamCommand : IRequest<bool>
    {
        public TeamResponse Team { get; set; }
    }
}
