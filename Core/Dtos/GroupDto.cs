namespace Core.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public TournamentDto Tournament { get; set; }
    }
}
