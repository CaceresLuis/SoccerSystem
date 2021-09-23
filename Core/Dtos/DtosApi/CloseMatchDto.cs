namespace Core.Dtos.DtosApi
{
    public class CloseMatchDto
    {
        public int IdMatch { get; set; }
        public int GoalsLocal { get; set; }
        public int GoalsVisitor { get; set; }
        public int LocalId { get; set; }
        public int VisitorId { get; set; }
        public int GroupId { get; set; }
    }
}
