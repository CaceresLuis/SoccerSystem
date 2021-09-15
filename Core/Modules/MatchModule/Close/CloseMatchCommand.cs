using MediatR;
using Core.Dtos;
using Core.ModelResponse;

namespace Core.Modules.MatchModule.Close
{
    public class CloseMatchCommand : IRequest<ActionResponse>
    {
        public MatchDto MatchDto { get; set; }
    }
}
