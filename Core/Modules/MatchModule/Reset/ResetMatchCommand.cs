using MediatR;
using Core.Dtos;
using Core.ModelResponse;

namespace Core.Modules.MatchModule.Reset
{
    public class ResetMatchCommand : IRequest<ActionResponse>
    {
        public MatchDto MatchDto { get; set; }
    }
}
