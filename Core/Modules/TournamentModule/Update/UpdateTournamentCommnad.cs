using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TournamentModule.Update
{
    public class UpdateTournamentCommnad : IRequest<ActionResponse>
    {
        public TournamentResponse TournamentResponse { get; set; }
    }
}
