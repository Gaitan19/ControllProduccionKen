using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class CatalogoCerchaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "IdLineaProduccion")]
        [Required(ErrorMessage = "El {0} es requerido")]
        public int IdLineaProduccion { get; set; } = 4;

        [Display(Name = "CodigoArticulo")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El {0} no puede exceder los 50 caracteres")]
        public string CodigoArticulo { get; set; } = null!;

        [Display(Name = "DescripcionArticulo")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(200, ErrorMessage = "El {0} no puede exceder los 200 caracteres")]
        public string DescripcionArticulo { get; set; } = null!;

        [Display(Name = "LongitudCentimetros")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El {0} debe ser un número positivo")]
        public decimal LongitudMetros { get; set; }

        [Display(Name = "Activo")]

        public bool Activo { get; set; }
    }
}
