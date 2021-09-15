using MediatR;
using Core.Dtos;

namespace Core.Modules.MatchModule.List
{
    public class ListMatchByGroupQuery : IRequest<GroupMatchsDto>
    {
        public int GroupId { get; set; }
    }
}
