using MediatR;
using Shared.ViewModel;
using Infrastructure.Models;

namespace Core.Modules.TournamentModule.Add
{
    public class AddTournamentCommand : IRequest<bool>
    {
        public TournamentViewModel<GroupEntity> Tournament { get; set; }
    }
}
