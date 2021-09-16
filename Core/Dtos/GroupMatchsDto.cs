using System.Collections.Generic;

namespace Core.Dtos
{
    public class GroupMatchsDto : GroupDto
    {
        public ICollection<MatchDto> Matches { get; set; }
    }
}
