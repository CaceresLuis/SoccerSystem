namespace Core.Dtos
{
    public class GroupMatchDto : GroupDto
    {
        public MatchDto Match { get; set; }
        public GroupTeamDto MyProperty { get; set; }
    }
}
