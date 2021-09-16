using System.Collections.Generic;

namespace Core.Dtos
{
    public class TournamentGroupsDto : TournamentDto
    {
        public ICollection<GroupDto> GroupDtos { get; set; }
    }
}
