using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class LineaProduccionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El {0} no puede exceder los 100 caracteres")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Activo")]

        public bool Activo { get; set; }
    }
}
