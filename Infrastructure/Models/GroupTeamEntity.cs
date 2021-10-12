using System;

namespace Infrastructure.Models
{
    public class GroupTeamEntity
    {
        public Guid Id { get; set; }
        public int MatchesPlayed { get; set; }
        public int MatchesWon { get; set; }
        public int MatchesTied { get; set; }
        public int MatchesLost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public GroupEntity Group { get; set; }
        public TeamEntity Team { get; set; }
    }
}
