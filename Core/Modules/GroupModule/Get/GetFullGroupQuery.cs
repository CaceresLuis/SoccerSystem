using MediatR;
using Core.ModelResponse.One;

namespace Core.Modules.GroupModule.Get
{
    public class GetFullGroupQuery : IRequest<OneGroupResponse>
    {
        public int Id { get; set; }
    }
}
