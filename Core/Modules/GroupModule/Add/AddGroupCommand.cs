using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupModule.Add
{
    public class AddGroupCommand : IRequest<ActionResponse>
    {
        public GroupResponse Group { get; set; }
    }
}
