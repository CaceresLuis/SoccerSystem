using MediatR;
using Core.ModelResponse.One;

namespace Core.Modules.GroupDetailsModule.Get
{
    public class GetGroupDetailsQuery : IRequest<OneGroupDetailsResponse>
    {
        public int Id { get; set; }
    }
}
