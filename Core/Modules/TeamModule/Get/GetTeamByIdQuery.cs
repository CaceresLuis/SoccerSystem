using MediatR;
using Core.ModelResponse.One;

namespace Core.Modules.TeamModule.Get
{
    public class GetTeamByIdQuery : IRequest<OneTeamResponse>
    {
        public int TeamId { get; set; }
    }
}
