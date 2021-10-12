using MediatR;
using Core.Dtos;

namespace Core.Modules.TournamentModule.Get
{
    public class GetTournamentByNameQuery : IRequest<TournamentDto>
    {
        public string Name { get; set; }
    }
}
