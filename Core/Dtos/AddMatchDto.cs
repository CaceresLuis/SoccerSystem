using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Dtos
{
    public class AddMatchDto
    {
        public int VisitorId { get; set; }
        public int LocalId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Hour { get; set; }
        public int GroupId { get; set; }
        public GroupDto Group { get; set; }
        public IEnumerable<SelectListItem> Team { get; set; }
    }
}
