using MediatR;
using Core.ModelResponse;
using Core.ModelResponse.One;

namespace Core.Modules.GroupDetailsModule.Update
{
    public class UpdateGroupDetailsCommand : IRequest<ActionResponse>
    {
        public AGroupDetailResponse GroupDetail { get; set; }
    }
}
