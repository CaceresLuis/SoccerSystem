using MediatR;
using Core.Dtos;

namespace Core.Modules.MatchModule.Get
{
    public class GetMatchQuery : IRequest<MatchDto>
    {
        public int Id { get; set; }
    }
}
