using MediatR;
using Core.Dtos;

namespace Core.Modules.MatchModule.Add
{
    public class AddMatchCommand : IRequest<bool>
    {
        public AddMatchDto AddMatchDto { get; set; }
    }
}
