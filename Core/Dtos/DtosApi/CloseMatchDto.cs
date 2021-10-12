using System;

namespace Core.Dtos.DtosApi
{
    public class CloseMatchDto
    {
        public Guid IdMatch { get; set; }
        public int GoalsLocal { get; set; }
        public int GoalsVisitor { get; set; }
        public Guid LocalId { get; set; }
        public Guid VisitorId { get; set; }
        public Guid GroupId { get; set; }
    }
}
