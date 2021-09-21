using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupModule.Get
{
    public class GetFullGroupQuery : IRequest<GroupResponse>
    {
        public int Id { get; set; }
    }
}
