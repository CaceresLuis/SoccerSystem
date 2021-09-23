using MediatR;

namespace Core.Modules.GroupModule.Remove
{
    public class RemoveGroupCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
