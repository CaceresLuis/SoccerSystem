using MediatR;
using Core.Dtos.DtosApi;

namespace Core.Modules.MatchModule.Close
{
    public class CloseMatchCommand : IRequest<bool>
    {
        public CloseMatchDto CloseMatchDto { get; set; }
    }
}
