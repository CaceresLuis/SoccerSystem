using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TournamentModule.Remove
{
    public class RemoveTournamentCommand : IRequest<ActionResponse>
    {
        public int Id { get; set; }
    }
}
