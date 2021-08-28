using MediatR;
using Infrastructure.Models;

namespace Core.Modules.TeamModule.List
{
    public class ListTeamsQuery : IRequest<TeamEntity[]>
    {
    }
}
