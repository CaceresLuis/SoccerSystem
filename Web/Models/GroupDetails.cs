namespace Web.Models
{
    public class GroupDetails
    {
        public int Id { get; set; }
        public int MatchesPlayed { get; set; }
        public int MatchesWon { get; set; }
        public int MatchesTied { get; set; }
        public int MatchesLost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public Group Group { get; set; }
        public Team Team { get; set; }
        public int Points => MatchesWon * 3 + MatchesTied;
        public int GoalDifference => GoalsFor - GoalsAgainst;
    }
}
