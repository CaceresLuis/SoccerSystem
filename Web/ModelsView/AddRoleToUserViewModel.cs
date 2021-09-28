using Core.Dtos;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.ModelsView
{
    public class AddRoleToUserViewModel
    {
        public UserDto UserDto { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<SelectListItem> SelectRol { get; set; }
    }
}
