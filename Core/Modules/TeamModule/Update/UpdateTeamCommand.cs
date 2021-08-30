using MediatR;
using Shared.ViewModel;

namespace Core.Modules.TeamModule.Update
{
    public class UpdateTeamCommand : IRequest<bool>
    {
        public TeamViewModel TeamViewModel { get; set; }
    }
}
