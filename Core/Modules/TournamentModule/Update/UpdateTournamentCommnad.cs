using MediatR;
using Core.Dtos;

namespace Core.Modules.TournamentModule.Update
{
    public class UpdateTournamentCommnad : IRequest<bool>
    {
        public TournamentDto TournamentDto { get; set; }
    }
}
