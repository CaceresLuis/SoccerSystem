using MediatR;
using Core.Dtos;

namespace Core.Modules.GroupModule.Update
{
    public class UpdateGroupCommand : IRequest<bool>
    {
        public GroupDto Group { get; set; }
    }
}
