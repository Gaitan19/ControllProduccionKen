using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class SubDetPrdBloqueViewModel 
    {
        public int Id { get; set; }

        [Display(Name = "Detalle Producción Bloque")]
        public int? DetPrdBloquesId { get; set; }
        public  int? subDetId { get; set; }

        [Required(ErrorMessage = "El campo Artículo es obligatorio")]
        [Display(Name = "Artículo")]
        public int IdArticulo { get; set; }
        public string? Articulo { get; set; }

        [Required(ErrorMessage = "El campo No. es obligatorio")]
        [Display(Name = "No.")]
        [Range(1, int.MaxValue, ErrorMessage = "El No. debe ser mayor a 0")]
        public int No { get; set; }

        [Required(ErrorMessage = "El campo Hora es obligatorio")]
        [Display(Name = "Hora")]
        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; }

        [Required(ErrorMessage = "El campo Silo es obligatorio")]
        [Display(Name = "Silo")]
        [Range(1, int.MaxValue, ErrorMessage = "El Silo debe ser mayor a 0")]
        public int Silo { get; set; }

        [Required(ErrorMessage = "El campo Densidad es obligatorio")]
        [Display(Name = "Densidad")]
        public int IdDensidad { get; set; }
        public string? Densidad { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Bloque es obligatorio")]
        [Display(Name = "Tipo de Bloque")]
        public int IdTipoBloque { get; set; }
        public string? TipoBloque { get; set; }

        [Required(ErrorMessage = "El campo Peso es obligatorio")]
        [Display(Name = "Peso")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El Peso debe ser mayor a 0")]
        public decimal Peso { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Fabricación es obligatorio")]
        [Display(Name = "Tipo de Fabricación")]
        public int IdTipoFabricacion { get; set; }
        public string? TipoFabricacion { get; set; }

        [Display(Name = "Número de Pedido")]
        public int? NumeroPedido { get; set; }

        [Required(ErrorMessage = "El campo Código de Bloque es obligatorio")]
        [Display(Name = "Código de Bloque")]
        [StringLength(100, ErrorMessage = "El Código de Bloque no puede exceder 100 caracteres")]
        public string CodigoBloque { get; set; } = string.Empty;

        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        [StringLength(500, ErrorMessage = "Las Observaciones no pueden exceder 500 caracteres")]
        public string? Observaciones { get; set; }

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        //// Validación cruzada
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    // Validar que la hora esté en un rango razonable (6 AM a 11 PM)
        //    if (Hora < TimeSpan.FromHours(6) || Hora > TimeSpan.FromHours(23))
        //        yield return new ValidationResult(
        //            "La hora debe estar entre las 6:00 AM y las 11:00 PM.",
        //            new[] { nameof(Hora) });

        //    // Validar que el código de bloque no esté vacío
        //    if (string.IsNullOrWhiteSpace(CodigoBloque))
        //        yield return new ValidationResult(
        //            "El código de bloque es obligatorio.",
        //            new[] { nameof(CodigoBloque) });

        //    // Validar que el peso sea razonable (no mayor a 10000 kg)
        //    if (Peso > 10000)
        //        yield return new ValidationResult(
        //            "El peso no puede ser mayor a 10,000 kg.",
        //            new[] { nameof(Peso) });

        //    // Validar que el silo esté en un rango razonable
        //    if (Silo < 1 || Silo > 50)
        //        yield return new ValidationResult(
        //            "El silo debe estar entre 1 y 50.",
        //            new[] { nameof(Silo) });
        //}
    }
}