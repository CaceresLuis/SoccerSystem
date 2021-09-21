using MediatR;
using Core.Dtos;

namespace Core.Modules.MatchModule.Close
{
    public class CloseMatchCommand : IRequest<bool>
    {
        public MatchDto MatchDto { get; set; }
    }
}
