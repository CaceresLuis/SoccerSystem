namespace Core.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TournamentDto TournamentDto { get; set; }
    }
}
