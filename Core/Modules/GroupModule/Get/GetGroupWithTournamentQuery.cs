using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupModule.Get
{
    public class GetGroupWithTournamentQuery : IRequest<GroupResponse>
    {
        public int Id { get; set; }
    }
}
