using System;

namespace Core.ModelResponse
{
    public class MatchResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TeamResponse Local { get; set; }
        public TeamResponse Visitor { get; set; }
        public int? GoalsLocal { get; set; }
        public int? GoalsVisitor { get; set; }
        public bool IsClosed { get; set; }
        public GroupResponse Group { get; set; }
        public DateTime DateLocal => Date.ToLocalTime();
    }
}
