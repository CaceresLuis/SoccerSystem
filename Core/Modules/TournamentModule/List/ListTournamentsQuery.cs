using MediatR;
using Shared.ViewModel;
using Infrastructure.Models;

namespace Core.Modules.TournamentModule.List
{
    public class ListTournamentsQuery : IRequest<TournamentEntity[]>
    {
    }
}
