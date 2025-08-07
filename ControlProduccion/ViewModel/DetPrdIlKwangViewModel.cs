using System;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class DetPrdIlKwangViewModel
    {
        public int Id { get; set; }

        public int? detrpdId { get; set; }

        public int? PrdIlKwangId { get; set; }

        public int? Posicion { get; set; }

        [Required(ErrorMessage = "El campo Espesor es obligatorio")]
        [Display(Name = "Espesor")]
        public int IdEspesor { get; set; }

        [Required(ErrorMessage = "El campo Cantidad es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "La Cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El campo Medida es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "La Medida debe ser mayor a 0")]
        public decimal Medida { get; set; }

        public decimal? MetrosCuadrados { get; set; }

        [Required(ErrorMessage = "El campo Status es obligatorio")]
        [Display(Name = "Status")]
        public int IdStatus { get; set; }

        [Required(ErrorMessage = "El campo Tipo es obligatorio")]
        [Display(Name = "Tipo")]
        public int IdTipo { get; set; }

        public string? IdUsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
