using MediatR;
using Core.Dtos;

namespace Core.Modules.GroupModule.Add
{
    public class AddGroupCommand : IRequest<bool>
    {
        public LiteGroupDto GroupDto { get; set; }
    }
}
