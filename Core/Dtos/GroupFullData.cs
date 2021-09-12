using System.Collections.Generic;

namespace Core.Dtos
{
    public class GroupFullData : GroupDto
    {
        public ICollection<MatchDto> MatchDtos { get; set; }
        public ICollection<GroupTeam> GroupTeams { get; set; }
    }
}
