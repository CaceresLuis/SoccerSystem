using System;
using MediatR;
using Core.Dtos;

namespace Core.Modules.MatchModule.Get
{
    public class GetGroupDetailsforMatchQuery : IRequest<AddMatchDto>
    {
        public Guid GroupId { get; set; }
    }
}
