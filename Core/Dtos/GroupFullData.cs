using System.Collections.Generic;

namespace Core.Dtos
{
    public class GroupFullData : GroupDto
    {
        public ICollection<MatchDto> Matches { get; set; }
        public ICollection<GroupTeamDto> GroupTeams { get; set; }
    }
}
