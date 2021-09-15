﻿using MediatR;
using Core.Dtos;
using Core.ModelResponse;

namespace Core.Modules.MatchModule.Add
{
    public class AddMatchCommand : IRequest<ActionResponse>
    {
        public AddMatchDto AddMatchDto { get; set; }
    }
}