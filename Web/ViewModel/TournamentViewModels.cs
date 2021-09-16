using System;
using Web.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Web.ViewModel
{
    public class TournamentViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IFormFile LogoFile { get; set; }

        public string LogoPath { get; set; }

        public bool IsActive { get; set; }

        //se aplica "GroupEntity" que se recibio
        public ICollection<Group> Groups { get; set; }

        public DateTime StartDateLocal => StartDate.ToLocalTime();

        public DateTime EndDateLocal => EndDate.ToLocalTime();
    }
}
