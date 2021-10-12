using System;
using MediatR;

namespace Core.Modules.TeamModule.Remove
{
    public class RemoveTeamCommand : IRequest<bool>
    {
        public Guid IdTeam { get; set; }
    }
}
