using MediatR;
using Core.ModelResponse;

namespace Core.Modules.TeamModule.List
{
    public class ListTeamsQuery : IRequest<Team[]>
    {
    }
}
