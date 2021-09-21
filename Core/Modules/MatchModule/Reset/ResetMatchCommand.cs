using MediatR;
using Core.Dtos;

namespace Core.Modules.MatchModule.Reset
{
    public class ResetMatchCommand : IRequest<bool>
    {
        public MatchDto MatchDto { get; set; }
    }
}
