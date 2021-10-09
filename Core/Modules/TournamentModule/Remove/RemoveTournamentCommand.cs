using System;
using MediatR;

namespace Core.Modules.TournamentModule.Remove
{
    public class RemoveTournamentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
