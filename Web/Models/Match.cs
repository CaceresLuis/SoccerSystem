using System;

namespace Web.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Team Local { get; set; }
        public Team Visitor { get; set; }
        public int GoalsLocal { get; set; }
        public int GoalsVisitor { get; set; }
        public bool IsClosed { get; set; }
        public DateTime DateLocal => Date.ToLocalTime();
        public Group Group { get; set; }
        //public ICollection<PredictionEntity> Predictions { get; set; }
    }
}
