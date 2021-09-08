using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupDetailsModule.Update
{
    public class UpdateGroupDetailsCommand : IRequest<ActionResponse>
    {
        public GroupDetail GroupDetail { get; set; }
    }
}
