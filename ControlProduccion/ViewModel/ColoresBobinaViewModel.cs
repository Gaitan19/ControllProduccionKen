using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class ColoresBobinaViewModel
    {
        public int Id { get; set; }
        [Display(Name ="Color")]
        [Required(ErrorMessage ="{0} es requerido")]
        public string Color { get; set; } = null!;
        [Display(Name = "Ral")]
        public string? Ral { get; set; }
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
    }
}
