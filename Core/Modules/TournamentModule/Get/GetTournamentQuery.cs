using MediatR;
using Core.ModelResponse.One;

namespace Core.Modules.TournamentModule.Get
{
    public class GetTournamentQuery : IRequest<OneTournamentResponse>
    {
        public int Id { get; set; }
    }
}
