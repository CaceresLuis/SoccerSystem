using System.Collections.Generic;

namespace Web.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Tournament Tournament { get; set; }
        public ICollection<Match> Matches { get; set; }
        public ICollection<GroupDetails> GroupDetails { get; set; }
    }
}
