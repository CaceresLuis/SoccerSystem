using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Core.Modules.RoleModule.Add
{
    public class AddRoleCommand : IRequest<bool>
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Name { get; set; }
    }
}
