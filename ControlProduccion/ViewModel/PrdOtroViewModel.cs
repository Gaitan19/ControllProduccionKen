using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class PrdOtroViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Operadores es obligatorio")]
        [Display(Name = "Operadores")]
        public List<string> IdUsuarios { get; set; } = new List<string>();

        public IEnumerable<SelectListItem>? Operarios { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        [Display(Name = "Aprobado por Supervisor")]
        public bool? AprobadoSupervisor { get; set; }
        public string? IdAprobadoSupervisor { get; set; }

        [Display(Name = "Aprobado por Gerencia")]
        public bool? AprobadoGerencia { get; set; }
        public string? IdAprobadoGerencia { get; set; }

        [Display(Name = "Tipo de Fabricacion")]
        public IEnumerable<SelectListItem>? TiposFabricacion { get; set; }

        [Display(Name = "Tiempo de paro (horas)")]
        [Range(0, double.MaxValue, ErrorMessage = "El tiempo de paro debe ser mayor o igual a 0")]
        public decimal? TiempoParo { get; set; }

        // Detalles
        public List<DetPrdOtroViewModel>? DetPrdOtros { get; set; }
    }

    public class DetPrdOtroViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Producción Otro")]
        public int PrdOtroId { get; set; }
        public int? detrpdId { get; set; }

        [Required(ErrorMessage = "El campo Actividad es obligatorio")]
        [Display(Name = "Actividad")]
        public string Actividad { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Descripción del Producto es obligatorio")]
        [Display(Name = "Descripción Producto")]
        public string DescripcionProducto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Tipo de Fabricación es obligatorio")]
        [Display(Name = "Tipo de Fabricación")]
        public int IdTipoFabricacion { get; set; }
        public string? TipoFabricacion { get; set; }

        [Display(Name = "Número de Pedido")]
        public int? NumeroPedido { get; set; }

        [Required(ErrorMessage = "El campo Nota es obligatorio")]
        [Display(Name = "Nota")]
        public string Nota { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Merma es obligatorio")]
        [Display(Name = "Merma")]
        public string Merma { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo Comentario es obligatorio")]
        [Display(Name = "Comentario")]
        public string Comentario { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Hora de Inicio es obligatoria")]
        [Display(Name = "Hora de Inicio")]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "La Hora de Fin es obligatoria")]
        [Display(Name = "Hora de Fin")]
        [DataType(DataType.Time)]
        public TimeSpan HoraFin { get; set; }

        [Required(ErrorMessage = "El campo Cantidad es obligatorio")]
        [Display(Name = "Cantidad")]
        [Range(0, double.MaxValue, ErrorMessage = "La cantidad debe ser mayor o igual a 0")]
        public decimal Cantidad { get; set; }

        [Required(ErrorMessage = "El campo Unidad de Medida es obligatorio")]
        [Display(Name = "Unidad de Medida")]
        public string UnidadMedida { get; set; } = string.Empty;

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}