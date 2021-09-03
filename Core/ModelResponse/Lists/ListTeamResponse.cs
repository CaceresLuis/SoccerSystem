using System.Collections.Generic;

namespace Core.ModelResponse.Lists
{
    public class ListTeamResponse
    {
        public IEnumerable<Team> Teams { get; set; }
        public ActionResponse Data { get; set; }
    }
}
