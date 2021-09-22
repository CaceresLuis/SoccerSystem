using System.Collections.Generic;

namespace Core.Dtos
{
    public class TournamentGroupsDto : TournamentDto
    {
        public ICollection<GroupDto> Group { get; set; }
    }
}
