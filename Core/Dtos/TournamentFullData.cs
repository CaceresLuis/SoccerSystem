using Core.Dtos.DtosApi;
using System;
using System.Collections.Generic;

namespace Core.Dtos
{
    public class TournamentFullData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string LogoPath { get; set; }
        public ICollection<GroupFullDataApi> Groups { get; set; }
        public DateTime EndDateLocal => EndDate.ToLocalTime();
        public DateTime StartDateLocal => StartDate.ToLocalTime();
    }
}
