using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TournamentModule.Get
{
    public class GetTournamentQuery : IRequest<TournamentResponse>
    {
        public int Id { get; set; }
    }
}
