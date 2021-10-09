using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Dtos
{
    public class AddGroupTeamDto
    {
        public Guid TeamId { get; set; }
        public Guid IdGroup { get; set; }
        public GroupDto Group { get; set; }
        public IEnumerable<SelectListItem> SelectTeam { get; set; }
    }
}
