﻿namespace Core.Dtos
{
    public class GroupMatchDto : GroupDto
    {
        public MatchDto MatchDto { get; set; }
        public GroupTeam MyProperty { get; set; }
    }
}