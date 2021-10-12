using MediatR;
using Core.Dtos;
using AutoMapper;
using System.Threading;
using Infrastructure.Models;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.Modules.TeamModule.List
{
    public class ListTeamsHandler : IRequestHandler<ListTeamsQuery, TeamDto[]>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IImageRepository _imageRepository;

        public ListTeamsHandler(ITeamRepository teamRepository, IMapper mapper, IGroupRepository groupRepository, IImageRepository imageRepository)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _groupRepository = groupRepository;
            _imageRepository = imageRepository;
        }

        public async Task<TeamDto[]> Handle(ListTeamsQuery request, CancellationToken cancellationToken)
        {
            List<GroupEntity> groupTournament = await _groupRepository.GetGroupTeamTournamentsAsync();
            List<TeamEntity> teams = await _teamRepository.GetAllTeamAsync();
            TeamDto[] teamDtos = _mapper.Map<TeamDto[]>(teams);

            foreach (TeamEntity team in teams)
            {
                ImageEntity img = await _imageRepository.GetImage(team.Id);
                foreach (TeamDto dto in teamDtos)
                {
                    if(team.Id == dto.Id)
                        dto.LogoPath = img.Path;
                }
            }

            if (groupTournament != null)
            {
                foreach (GroupEntity group in groupTournament)
                {
                    foreach (GroupTeamEntity detail in group.GroupTeams)
                    {
                        TeamEntity team = detail.Team;
                        TeamEntity teamList = teams.Find(t => t.Name == team.Name);
                        if (teamList != null)
                            teams.Remove(teamList);
                    }
                }
            }

            return teamDtos;
        }
    }
}
