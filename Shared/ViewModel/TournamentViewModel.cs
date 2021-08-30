using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.ViewModel
{
    //se recibe la entidad "GroupEntity" cuando se llama a esta clase
    //con esto se evita  que la capa Shared conozca la capa Infrastructure :D
    public class TournamentViewModel<T>
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
        public ICollection<T> Groups { get; set; }
    }
}
