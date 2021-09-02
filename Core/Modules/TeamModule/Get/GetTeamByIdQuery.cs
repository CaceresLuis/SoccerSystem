using MediatR;
using Shared.ViewModel;

namespace Core.Modules.TeamModule.Get
{
    public class GetTeamByIdQuery : IRequest<TeamViewModel>
    {
        public int TeamId { get; set; }
    }
}
