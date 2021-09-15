﻿namespace Core.ModelResponse.One
{
    public class AGroupDetailResponse
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
        public TeamResponse Team { get; set; }
        public GroupResponse Group { get; set; }
    }
}