using Web.Models;
using Core.ModelResponse;

namespace Web.ViewModel
{
    public class TournamentViewModel
    {
        public Tournament Tournament { get; set; }
        public ActionResponse Data { get; set; }
    }
}
