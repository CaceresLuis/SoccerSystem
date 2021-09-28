using MediatR;
using Core.Dtos;

namespace Core.Modules.TournamentModule.Get
{
    public class GetTournamentQuery : IRequest<TournamentFullData>
    {
        public int Id { get; set; }
    }
}
