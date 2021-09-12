using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Dtos
{
    public class AddMatchDto
    {
        public int VisitorId { get; set; }
        public int LocalId { get; set; }
        public GroupDto GroupDto { get; set; }
        public IEnumerable<SelectListItem> SelectVisitor { get; set; }
        public IEnumerable<SelectListItem> SelectLocal { get; set; }
    }
}
