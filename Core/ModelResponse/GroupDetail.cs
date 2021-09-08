namespace Core.ModelResponse
{
    public class GroupDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MatchesPlayed { get; set; }
        public int MatchesWon { get; set; }
        public int MatchesLost { get; set; }
        public int MatchesTied { get; set; }
        public int Points => MatchesWon * 3 + MatchesTied;
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference => GoalsFor - GoalsAgainst;
        public Team Team { get; set; }
        public Group Group { get; set; }
    }
}
