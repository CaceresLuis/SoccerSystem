using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TournamentModule.Add
{
    public class AddTournamentCommand : IRequest<bool>
    {
        public TournamentResponse Tournament { get; set; }
    }
}
