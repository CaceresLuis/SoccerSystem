using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ModelsView
{
    public class AddMatchViewModel
    {
        public int VisitorId { get; set; }
        public int LocalId { get; set; }
        public DateTime Date { get; set; }
        public GroupViewModel Group { get; set; }
        public IEnumerable<SelectListItem> Team { get; set; }
    }
}
