using System.Collections.Generic;

namespace Core.ModelResponse.Lists
{
    public class ListTournamentResponse
    {
        public IEnumerable<Tournament> Tournaments { get; set; }
        public ActionResponse Data { get; set; }
    }
}
