using MediatR;
using Infrastructure.Models;

namespace Core.Modules.MatchModule.Reset
{
    public class ResetMatchCommand : IRequest<bool>
    {
        public MatchEntity Match { get; set; }
    }
}
