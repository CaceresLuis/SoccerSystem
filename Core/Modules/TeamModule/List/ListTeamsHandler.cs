using MediatR;
using AutoMapper;
using System.Threading;
using Core.ModelResponse;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Core.Modules.TeamModule.List
{
    public class ListTeamsHandler : IRequestHandler<ListTeamsQuery, TeamResponse[]>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupRepository _groupRepository;

        public ListTeamsHandler(ITeamRepository teamRepository, IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _groupRepository = groupRepository;
        }

        public async Task<TeamResponse[]> Handle(ListTeamsQuery request, CancellationToken cancellationToken)
        {
            List<GroupEntity> groupTournament = await _groupRepository.GetGroupTeamTournamentsAsync();
            List<TeamEntity> teams = await _teamRepository.GetAllTeamAsync();
            foreach (GroupEntity group in groupTournament)
            {
                foreach (GroupTeamEntity detail in group.GroupDetails)
                {
                    TeamEntity team = detail.Team;
                    TeamEntity teamList = teams.Find(t => t.Name == team.Name);
                    if (teamList != null)
                        teams.Remove(teamList);
                }
            }

            return _mapper.Map<TeamResponse[]>(teams);
        }
    }
}
