namespace Core.Dtos
{
    public class GroupMatchDto : GroupDto
    {
        public MatchDto Match { get; set; }
        public GroupTeam MyProperty { get; set; }
    }
}
