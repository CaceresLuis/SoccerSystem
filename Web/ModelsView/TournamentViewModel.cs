using System;

namespace Web.ModelsView
{
    public class TournamentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartDateLocal => StartDate.ToLocalTime();
        public DateTime EndDate { get; set; }
        public DateTime EndDateLocal => EndDate.ToLocalTime();
        public bool IsActive { get; set; }
        public string LogoPath { get; set; }
    }
}
