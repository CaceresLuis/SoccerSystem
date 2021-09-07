using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupDetailsModule.Add
{
    public class AddGroupDetailsCommand : IRequest<ActionResponse>
    {
        public GroupDetailsResponse GroupDetail { get; set; }
    }
}
