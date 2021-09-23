using MediatR;

namespace Core.Modules.GroupTeamModule.Remove
{
    public class RemoveGroupDetailCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
