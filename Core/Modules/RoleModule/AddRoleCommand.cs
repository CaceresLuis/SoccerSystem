using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Core.Modules.RoleModule
{
    public class AddRoleCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Name { get; set; }
    }
}
