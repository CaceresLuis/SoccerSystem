using System;

namespace Core.Dtos
{
    public class MatchDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int GoalsLocal { get; set; }
        public int GoalsVisitor { get; set; }
        public bool IsClosed { get; set; }
        public TeamDto Local { get; set; }
        public TeamDto Visitor { get; set; }
        public int GroupId { get; set; }
        public DateTime DateLocal => Date.ToLocalTime();
    }
}
