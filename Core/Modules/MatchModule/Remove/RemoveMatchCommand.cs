﻿using MediatR;

namespace Core.Modules.MatchModule.Remove
{
    public class RemoveMatchCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
