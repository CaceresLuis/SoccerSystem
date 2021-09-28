using MediatR;

namespace Core.Modules.TournamentModule.Remove
{
    public class RemoveTournamentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
