using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupModule.Update
{
    public class UpdateGroupCommand : IRequest<ActionResponse>
    {
        public Group Group { get; set; }
    }
}
