using MediatR;
using Core.Dtos;

namespace Core.Modules.TournamentModule.List
{
    public class ListTournamentsQuery : IRequest<TournamentFullData[]>
    {
    }
}
