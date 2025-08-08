using System;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class DetPrdCortePViewModel
    {
        //debido a que la tabla padre ya tiene un id, se nombra diferente este id para que no genere conflicto en el fronted. pero este campo es el Id(DetPrdCortePId) del detalle
        public int DetPrdCortePId { get; set; }

        [Display(Name = "Producci�n Corte P")]
        public int PrdCortePid { get; set; }

        [Display(Name = "No.")]
        [Range(1, int.MaxValue, ErrorMessage = "El No. debe ser mayor a 0")]
        public int No { get; set; }

        [Required(ErrorMessage = "El campo Art�culo es obligatorio")]
        [Display(Name = "Art�culo")]
        public int IdArticulo { get; set; }
        public string? Articulo { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Fabricaci�n es obligatorio")]
        [Display(Name = "Tipo de Fabricaci�n")]
        public int IdTipoFabricacion { get; set; }
        public string? TipoFabricacion { get; set; }
        public int? NumeroPedido { get; set; }

        [Required(ErrorMessage = "El campo C�digo de Bloque es obligatorio")]
        [Display(Name = "C�digo de Bloque")]
        [StringLength(200, ErrorMessage = "El C�digo de Bloque no puede exceder 200 caracteres")]
        public string? PrdCodigoBloque { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Densidad es obligatorio")]
        [Display(Name = "Densidad")]
        public int IdDensidad { get; set; }
        public string? Densidad { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Bloque es obligatorio")]
        [Display(Name = "Tipo de Bloque")]
        public int IdTipoBloque { get; set; }
        public string? TipoBloque { get; set; }

        [Display(Name = "Piezas Conformes")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ser un n�mero positivo")]
        public decimal CantidadPiezasConformes { get; set; }

        [Display(Name = "Piezas No Conformes")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ser un n�mero positivo")]
        public decimal CantidadPiezasNoConformes { get; set; }

        [Display(Name = "Nota")]
        [StringLength(200, ErrorMessage = "La nota no puede exceder 200 caracteres")]
        public string? Nota { get; set; }

        // Auditor�a
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}