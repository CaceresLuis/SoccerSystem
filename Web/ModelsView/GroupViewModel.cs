namespace Web.ModelsView
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TournamentViewModel Tournament { get; set; }
    }
}
