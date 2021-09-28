using MediatR;
using Core.Dtos;

namespace Core.Modules.GroupModule.Add
{
    public class AddGroupCommand : IRequest<bool>
    {
        public GroupDto GroupDto { get; set; }
    }
}
