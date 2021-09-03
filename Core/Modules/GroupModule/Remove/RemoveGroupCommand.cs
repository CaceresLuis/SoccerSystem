using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupModule.Remove
{
    public class RemoveGroupCommand : IRequest<ActionResponse>
    {
        public int Id { get; set; }
    }
}
