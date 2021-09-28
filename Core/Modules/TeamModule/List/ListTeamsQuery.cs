using MediatR;
using Core.Dtos;

namespace Core.Modules.TeamModule.List
{
    public class ListTeamsQuery : IRequest<TeamDto[]>
    {
    }
}
