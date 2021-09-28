using MediatR;
using Core.Dtos.DtosApi;

namespace Core.Modules.TournamentModule.Add
{
    public class AddTournamentCommand : IRequest<bool>
    {
        public AddTournamentDto Tournament { get; set; }
    }
}
