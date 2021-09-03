using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupModule.Get
{
    public class GetGroupWithTournamentQuery : IRequest<Group>
    {
        public int Id { get; set; }
    }
}
