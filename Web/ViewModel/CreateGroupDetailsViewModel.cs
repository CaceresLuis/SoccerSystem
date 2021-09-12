using Web.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ViewModel
{
    public class CreateGroupDetailsViewModel
    {
        public int TeamId { get; set; }
        public Group Group { get; set; }
        public IEnumerable<SelectListItem> SelectTeam { get; set; }
    }
}
