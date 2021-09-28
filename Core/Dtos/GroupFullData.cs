using System.Collections.Generic;

namespace Core.Dtos
{
    public class GroupFullData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public TournamentDto Tournament { get; set; }
        public ICollection<MatchDto> Matches { get; set; }
        public ICollection<GroupTeamDto> GroupTeams { get; set; }
    }
}
