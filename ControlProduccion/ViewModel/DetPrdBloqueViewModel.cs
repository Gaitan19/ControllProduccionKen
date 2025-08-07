using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class DetPrdBloqueViewModel 
    {
        public int Id { get; set; }

        [Display(Name = "Producción Bloque")]
        public int PrdBloqueId { get; set; }
        public int? detrpdId { get; set; }

        [Display(Name = "Posición")]
        public int Posicion { get; set; }

        [Required(ErrorMessage = "El campo Máquina es obligatorio")]
        [Display(Name = "Máquina")]
        public int IdMaquina { get; set; }
        public string? Maquina { get; set; }

        [Required(ErrorMessage = "El campo Catálogo de Bloques es obligatorio")]
        [Display(Name = "Catálogo de Bloques")]
        public int IdCatBloque { get; set; }
        public string? CatBloque { get; set; }

        [Display(Name = "Producción del Día")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public decimal? ProduccionDia { get; set; }

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        // Sub-detalles
        public List<SubDetPrdBloqueViewModel>? SubDetPrdBloques { get; set; }

    }
}