using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class MatchEntity
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime Hour { get; set; }
        public TeamEntity Local { get; set; }
        public TeamEntity Visitor { get; set; }
        public int GoalsLocal { get; set; }
        public int GoalsVisitor { get; set; }
        public bool IsClosed { get; set; }
        public GroupEntity Group { get; set; }
        public ICollection<PredictionEntity> Predictions { get; set; }
    }
}
