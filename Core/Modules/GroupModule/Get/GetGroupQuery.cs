using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupModule.Get
{
    public class GetGroupQuery : IRequest<Group>
    {
        public int Id { get; set; }
    }
}
