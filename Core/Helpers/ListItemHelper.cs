using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Helpers
{
    public class ListItemHelper : IListItemHelper
    {
        private readonly IRoleRepository _roleRepository;

        public ListItemHelper(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<SelectListItem>> RolesListItem()
        {
            List<IdentityRole> identityRoles = await _roleRepository.GetRoles();
            IEnumerable<SelectListItem> roles = identityRoles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            });

            return roles;
        }
        public async Task<IEnumerable<SelectListItem>> RolesListItem(List<string> userRoles)
        {
            List<IdentityRole> identityRoles = await _roleRepository.GetRoles();
            identityRoles.ToString();
            foreach (string item in userRoles)
            {
                IdentityRole exist = identityRoles.Where(r => r.Name == item).FirstOrDefault();
                if (exist != null)
                    identityRoles.Remove(exist);
            }

            IEnumerable<SelectListItem> roles = identityRoles.Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Name
            });

            return roles;
        }
    }
}
