using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Usuario es requerido")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Password es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="El rol es requerido")]
        public string SelectedRole { get; set; }

        // Lista de roles disponibles para mostrar en un dropdown
        public IEnumerable<SelectListItem>? Roles { get; set; }
    }
}
