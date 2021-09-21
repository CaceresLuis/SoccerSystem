using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupModule.Add
{
    public class AddGroupCommand : IRequest<bool>
    {
        public GroupResponse Group { get; set; }
    }
}
