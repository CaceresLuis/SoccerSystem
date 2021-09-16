using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TournamentModule.Add
{
    public class AddTournamentCommand : IRequest<ActionResponse>
    {
        public TournamentResponse Tournament { get; set; }
    }
}
