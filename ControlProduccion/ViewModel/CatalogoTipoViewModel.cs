using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class CatalogoTipoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El {0} no puede exceder los 50 caracteres")]
        public string Descripcion { get; set; } = null!;

        [Display(Name = "Activo")]

        public bool Activo { get; set; }
    }
}
