using System;
using MediatR;

namespace Core.Modules.MatchModule.Remove
{
    public class RemoveMatchCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
