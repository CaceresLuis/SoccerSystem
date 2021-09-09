using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupDetailsModule.Update
{
    public class UpdateGroupDetailsCommand : IRequest<ActionResponse>
    {
        public GroupDetailResponse GroupDetail { get; set; }
    }
}
