using System.Collections.Generic;

namespace Core.Dtos
{
    public class GroupDetailsDto : GroupDto
    {
        public ICollection<GroupTeam> GroupTeams { get; set; }
    }
}
