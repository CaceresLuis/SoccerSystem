using MediatR;

namespace Core.Modules.GroupDetailsModule.Remove
{
    public class RemoveGroupDetailCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
