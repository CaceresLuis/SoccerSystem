using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TeamModule.Add
{
    public class AddTeamCommand : IRequest<bool>
    {
        public TeamResponse Team { get; set; }
    }
}
