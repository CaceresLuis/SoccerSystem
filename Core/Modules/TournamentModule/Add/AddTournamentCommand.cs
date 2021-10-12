using MediatR;
using Core.Dtos.AddDtos;

namespace Core.Modules.TournamentModule.Add
{
    public class AddTournamentCommand : IRequest<bool>
    {
        public AddTournamentDto Tournament { get; set; }
    }
}
