using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TournamentModule.List
{
    public class ListTournamentsQuery : IRequest<Tournament[]>
    {
    }
}
