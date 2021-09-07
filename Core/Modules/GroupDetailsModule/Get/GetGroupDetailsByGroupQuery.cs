using MediatR;
using Core.ModelResponse;

namespace Core.Modules.GroupDetailsModule.Get
{
    public class GetGroupDetailsByGroupQuery : IRequest<GroupDetailsResponse>
    {
        public int IdGroup { get; set; }
    }
}
