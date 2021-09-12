using System.Collections.Generic;

namespace Core.Dtos
{
    public class GroupMatchsDto : GroupDto
    {
        public ICollection<MatchDto> MatchDtos { get; set; }
    }
}
