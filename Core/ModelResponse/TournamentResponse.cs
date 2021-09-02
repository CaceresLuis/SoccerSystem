using System;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.ModelResponse
{
    public class TournamentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Logo")]
        public IFormFile LogoFile { get; set; }

        [Display(Name = "Logo")]
        public string LogoPath { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

        //se aplica "GroupEntity" que se recibio
        public ICollection<GroupEntity> Groups { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDateLocal => StartDate.ToLocalTime();

        [Display(Name = "End Date")]
        public DateTime EndDateLocal => EndDate.ToLocalTime();
        public ActionResponse Data { get; set; }
    }
}
