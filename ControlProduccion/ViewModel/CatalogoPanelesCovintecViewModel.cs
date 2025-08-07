using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class CatalogoPanelesCovintecViewModel
    {
        public int Id { get; set; }

        [Display(Name = "IdLineaProduccion")]
        [Required(ErrorMessage = "El {0} es requerido")]

        public int IdLineaProduccion { get; set; } = 4;

        [Display(Name = "DescripcionArticulo")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(200, ErrorMessage = "El {0} no puede exceder los 200 caracteres")]
        public string DescripcionArticulo { get; set; } = null!;

        [Display(Name = "CodigoArticulo")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El {0} no puede exceder los 50 caracteres")]
        public string CodigoArticulo { get; set; } = null!;

        [Display(Name = "Mts2PorPanel")]
        [Required(ErrorMessage = "El {0} es requerido")]
        [Range(0.0001, 9999999.9999, ErrorMessage = "El {0} debe estar entre 0.0001 y 9999999.9999")]
        public decimal Mts2PorPanel { get; set; }

        [Display(Name = "Activo")]

        public bool Activo { get; set; }
    }
}
