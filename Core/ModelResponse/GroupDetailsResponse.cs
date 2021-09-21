using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.ModelResponse
{
    public class GroupDetailsResponse
    {
        public int TeamId { get; set; }
        public GroupResponse Group { get; set; }
        public IEnumerable<SelectListItem> SelectTeam { get; set; }
    }
}
