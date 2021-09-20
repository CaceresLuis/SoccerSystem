using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupDetailsModule.Add
{
    public class AddGroupDetailsCommand : IRequest<bool>
    {
        public GroupDetailsResponse GroupDetail { get; set; }
    }
}
