using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public interface IListItemHelper
    {
        Task<IEnumerable<SelectListItem>> RolesListItem();
        Task<IEnumerable<SelectListItem>> RolesListItem(List<string> roles);
    }
}