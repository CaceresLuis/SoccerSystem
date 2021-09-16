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

        public DateTime DateLocal => Date.ToLocalTime();

        public TeamEntity Local { get; set; }

        public TeamEntity Visitor { get; set; }

        [Display(Name = "Goals Local")]
        public int GoalsLocal { get; set; }

        [Display(Name = "Goals Visitor")]
        public int GoalsVisitor { get; set; }

        [Display(Name = "Is Closed?")]
        public bool IsClosed { get; set; }

        public GroupEntity Group { get; set; }

        public ICollection<PredictionEntity> Predictions { get; set; }
    }
}
