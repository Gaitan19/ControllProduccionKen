using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; } // Nuevo password (opcional)

        [Required(ErrorMessage = "Debe seleccionar un rol.")]
        public string SelectedRole { get; set; }

        public IEnumerable<SelectListItem>? Roles { get; set; }
    }
}
