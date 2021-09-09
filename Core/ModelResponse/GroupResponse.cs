﻿using System.Collections.Generic;

namespace Core.ModelResponse
{
    public class GroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TournamentResponse Tournament { get; set; }
        public ICollection<MatchResponse> Matches { get; set; }
        public ICollection<GroupDetailResponse> GroupDetails { get; set; }
    }
}
