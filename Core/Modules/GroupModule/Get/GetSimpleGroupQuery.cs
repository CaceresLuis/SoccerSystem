using System;
using MediatR;
using Core.Dtos;

namespace Core.Modules.GroupModule.Get
{
    public class GetSimpleGroupQuery : IRequest<GroupDto>
    {
        public Guid Id { get; set; }
    }
}
