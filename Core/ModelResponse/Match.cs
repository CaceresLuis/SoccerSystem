using System;

namespace Core.ModelResponse
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Team Local { get; set; }
        public Team Visitor { get; set; }
        public int? GoalsLocal { get; set; }
        public int? GoalsVisitor { get; set; }
        public bool IsClosed { get; set; }
        public Group Group { get; set; }
        public DateTime DateLocal => Date.ToLocalTime();
    }
}
