using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupModule.Add
{
    public class AddGroupCommand : IRequest<ActionResponse>
    {
        public Group Group { get; set; }
    }
}
