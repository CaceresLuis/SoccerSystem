using MediatR;
using Core.Dtos;

namespace Core.Modules.GroupModule.Get
{
    public class GetGroupQuery : IRequest<GroupDto>
    {
        public int Id { get; set; }
    }
}
