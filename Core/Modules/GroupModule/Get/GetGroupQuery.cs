using System;
using MediatR;
using Core.Dtos.DtosApi;

namespace Core.Modules.GroupModule.Get
{
    public class GetGroupQuery : IRequest<GroupFullDataApi>
    {
        public Guid Id { get; set; }
    }
}
