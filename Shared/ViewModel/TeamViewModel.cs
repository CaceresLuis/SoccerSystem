using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.ViewModel
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Logo")]
        public IFormFile LogoFile { get; set; }

        public string LogoPath { get; set; }
        public string Name { get; set; }
    }
}
