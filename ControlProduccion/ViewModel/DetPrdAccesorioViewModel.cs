using System;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class DetPrdAccesorioViewModel
    {
        //debido a que la tabla padre ya tiene un id, se nombra diferente este id para que no genere conflicto en el frontend. pero este campo es el Id(DetPrdAccesorioId) del detalle
        public int DetPrdAccesorioId { get; set; }

        [Display(Name = "Producción Accesorio")]
        public int PrdAccesoriosId { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Artículo es obligatorio")]
        [Display(Name = "Tipo de Artículo")]
        public int IdTipoArticulo { get; set; }
        public string? TipoArticulo { get; set; }

        [Required(ErrorMessage = "El campo Artículo es obligatorio")]
        [Display(Name = "Artículo")]
        public int IdArticulo { get; set; }
        public string? Articulo { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Fabricación es obligatorio")]
        [Display(Name = "Tipo de Fabricación")]
        public int IdTipoFabricacion { get; set; }
        public string? TipoFabricacion { get; set; }

        [Display(Name = "Número de Pedido")]
        public int? NumeroPedido { get; set; }

        [Display(Name = "Malla Covintec")]
        public int? IdMallaCovintec { get; set; }
        public string? MallaCovintec { get; set; }

        [Display(Name = "Cantidad Malla (Un)")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public int? CantidadMallaUn { get; set; }

        [Display(Name = "Tipo de Malla PCH")]
        public int? IdTipoMallaPch { get; set; }
        public string? TipoMallaPch { get; set; }

        [Display(Name = "Cantidad PCH (Kg)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Formato inválido. Use hasta 2 decimales")]
        public decimal? CantidadPchKg { get; set; }

        [Display(Name = "Ancho Bobina")]
        public int? IdAnchoBobina { get; set; }
        public string? AnchoBobina { get; set; }

        [Display(Name = "Cantidad Bobina (Kg)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Formato inválido. Use hasta 2 decimales")]
        public decimal? CantidadBobinaKg { get; set; }

        [Display(Name = "Calibre")]
        public int? IdCalibre { get; set; }
        public string? Calibre { get; set; }

        [Display(Name = "Cantidad Calibre (Kg)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Formato inválido. Use hasta 2 decimales")]
        public decimal? CantidadCalibreKg { get; set; }

   

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}