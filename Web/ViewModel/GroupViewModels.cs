using Web.Models;
using System.Collections.Generic;

namespace Web.ViewModel
{
    public class GroupViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TournamentViewModels Tournament { get; set; }
        public ICollection<GroupDetails> GroupDetails { get; set; }
        //public ICollection<Match> Matches { get; set; }
    }
}
