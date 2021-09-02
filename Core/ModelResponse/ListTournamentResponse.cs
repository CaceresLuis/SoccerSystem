using System.Collections.Generic;

namespace Core.ModelResponse
{
    public class ListTournamentResponse
    {
        public IEnumerable<TournamentResponse> Tournaments { get; set; }
        public ActionResponse Data { get; set; }
    }
}
