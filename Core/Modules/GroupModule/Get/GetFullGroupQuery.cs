using MediatR;
using Core.ModelResponse.One;

namespace Core.Modules.GroupModule.Get
{
    public class GetFullGroupQuery : IRequest<AGroupResponse>
    {
        public int Id { get; set; }
    }
}
