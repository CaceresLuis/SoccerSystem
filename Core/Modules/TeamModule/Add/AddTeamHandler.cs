using Infrastructure.Interfaces;
using Infrastructure.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Modules.TeamModule.Add
{
    public class AddTeamHandler : IRequestHandler<AddTeamCommand, bool>
    {
        private readonly ITeamRepository _teamRepository;

        public AddTeamHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<bool> Handle(AddTeamCommand request, CancellationToken cancellationToken)
        {
            var team = new TeamEntity
            {
                Name = request.Name
            };

            return await _teamRepository.AddTeamAsync(team);
        }
    }
}
