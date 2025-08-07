using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class AnchoBobinaViewModel
    {
        public int Id { get; set; }
        [Display(Name ="Valor")]
        [Required(ErrorMessage ="El {0} es requerido")]
        public int Valor { get; set; }
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
    }
}
