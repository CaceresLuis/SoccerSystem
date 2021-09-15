using Web.Models;
using Core.ModelResponse;
using System.Collections.Generic;

namespace Web.ViewModel
{
    public class ListTournamentViewModels
    {
        public IEnumerable<Tournament> Tournaments { get; set; }
        public ActionResponse Data { get; set; }
    }
}
