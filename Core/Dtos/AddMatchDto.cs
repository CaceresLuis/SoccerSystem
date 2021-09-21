using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class AddMatchDto
    {
        public int VisitorId { get; set; }
        public int LocalId { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }
        [DisplayFormat(DataFormatString = "{HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime Hour { get; set; }
        public GroupDto Group { get; set; }
        public IEnumerable<SelectListItem> Team { get; set; }
    }
}
