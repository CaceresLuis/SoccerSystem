using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Dtos
{
    public class AddGroupTeamDto
    {
        public int TeamId { get; set; }
        public int IdGroup { get; set; }
        public GroupDto Group { get; set; }
        public IEnumerable<SelectListItem> SelectTeam { get; set; }
    }
}
