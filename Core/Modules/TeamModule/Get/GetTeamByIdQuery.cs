using MediatR;
using Core.Dtos;

namespace Core.Modules.TeamModule.Get
{
    public class GetTeamByIdQuery : IRequest<TeamDto>
    {
        public int TeamId { get; set; }
    }
}
