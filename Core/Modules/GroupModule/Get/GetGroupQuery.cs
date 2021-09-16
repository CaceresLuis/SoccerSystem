using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupModule.Get
{
    public class GetGroupQuery : IRequest<GroupResponse>
    {
        public int Id { get; set; }
    }
}
