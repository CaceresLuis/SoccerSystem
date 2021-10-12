using System;
using System.Collections.Generic;

namespace Core.Dtos.DtosApi
{
    public class GroupFullDataApi
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Guid TournamentId { get; set; }
        public ICollection<MatchDto> Matches { get; set; }
        public ICollection<GroupTeamDto> GroupTeams { get; set; }
    }
}
