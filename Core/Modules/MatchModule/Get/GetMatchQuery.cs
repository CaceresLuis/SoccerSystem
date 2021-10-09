using System;
using MediatR;
using Core.Dtos;

namespace Core.Modules.MatchModule.Get
{
    public class GetMatchQuery : IRequest<MatchDto>
    {
        public Guid Id { get; set; }
    }
}
