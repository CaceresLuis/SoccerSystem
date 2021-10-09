using Core.Dtos;
using MediatR;

namespace Core.Modules.TeamModule.Get
{
    public class GetTeamByNameQuery : IRequest<TeamDto>
    {
        public string TeamName { get; set; }
    }
}
