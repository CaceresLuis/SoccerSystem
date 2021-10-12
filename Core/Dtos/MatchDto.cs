using System;

namespace Core.Dtos
{
    public class MatchDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime Hour { get; set; }
        public int GoalsLocal { get; set; }
        public int GoalsVisitor { get; set; }
        public bool IsClosed { get; set; }
        public Guid LocalId { get; set; }
        public TeamDto Local { get; set; }
        public Guid VisitorId { get; set; }
        public TeamDto Visitor { get; set; }
        public Guid GroupId { get; set; }
    }
}
