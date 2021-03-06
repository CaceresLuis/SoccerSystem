using System;
using MediatR;
using Core.Dtos;

namespace Core.Modules.GroupModule.Get
{
    public class GetFullGroupQuery : IRequest<GroupFullData>
    {
        public Guid Id { get; set; }
    }
}
