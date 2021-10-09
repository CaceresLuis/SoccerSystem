using System;
using MediatR;
using Core.Dtos;

namespace Core.Modules.MatchModule.List
{
    public class ListMatchByGroupQuery : IRequest<GroupMatchsDto>
    {
        public Guid GroupId { get; set; }
    }
}
