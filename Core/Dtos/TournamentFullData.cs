using System.Collections.Generic;

namespace Core.Dtos
{
    public class TournamentFullData : TournamentDto
    {
        public ICollection<GroupFullData> groupFullDatas { get; set; }
    }
}
