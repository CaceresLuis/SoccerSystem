using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TournamentModule.Add
{
    public class AddTournamentCommand : IRequest<ActionResponse>
    {
        public Tournament Tournament { get; set; }
    }
}
