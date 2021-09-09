using Web.Models;
using Core.ModelResponse;
using System.Collections.Generic;

namespace Web.ViewModel
{
    public class ListTeamViewModel
    {
        public IEnumerable<Team> Teams { get; set; }
        public ActionResponse Data { get; set; }
    }
}
