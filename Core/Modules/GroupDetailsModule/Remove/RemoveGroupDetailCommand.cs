using MediatR;
using Core.ModelResponse.One;

namespace Core.Modules.GroupDetailsModule.Remove
{
    public class RemoveGroupDetailCommand : IRequest<RGroupDetailsResponse>
    {
        public int Id { get; set; }
    }
}
