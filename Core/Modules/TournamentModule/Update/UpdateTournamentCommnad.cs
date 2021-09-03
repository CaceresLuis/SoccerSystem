using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TournamentModule.Update
{
    public class UpdateTournamentCommnad : IRequest<ActionResponse>
    {
        public Tournament TournamentResponse { get; set; }
    }
}
