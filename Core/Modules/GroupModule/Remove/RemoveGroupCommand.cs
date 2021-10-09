using System;
using MediatR;

namespace Core.Modules.GroupModule.Remove
{
    public class RemoveGroupCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
