using MediatR;
using System;

namespace Core.Modules.GroupTeamModule.Remove
{
    public class RemoveGroupDetailCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
