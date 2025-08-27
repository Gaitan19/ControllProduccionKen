using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class PrdPreExpansionViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Operadores es obligatorio")]
        [Display(Name = "Operadores")]
        public List<string> IdUsuarios { get; set; } = new List<string>();
        public IEnumerable<SelectListItem>? Operarios { get; set; }

        [Required(ErrorMessage = "El campo Máquina es obligatorio")]
        [Display(Name = "Máquina")]
        public int IdMaquina { get; set; }
        public IEnumerable<SelectListItem>? Maquinas { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "La Hora de Inicio es obligatoria")]
        [Display(Name = "Hora de Inicio")]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "La Hora de Fin es obligatoria")]
        [Display(Name = "Hora de Fin")]
        [DataType(DataType.Time)]
        public TimeSpan HoraFin { get; set; }

        [Display(Name = "Presión Caldera")]
        public string? PresionCaldera { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Fabricación es obligatorio")]
        [Display(Name = "Tipo de Fabricación")]
        public int IdTipoFabricacion { get; set; }
        public IEnumerable<SelectListItem>? TiposFabricacion { get; set; }

        [Display(Name = "Número de Pedido")]
        public int? NumeroPedido { get; set; }

        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string? Observaciones { get; set; }

        [Display(Name = "Tiempo Paro (Horas)")]
        public decimal? TiempoParo { get; set; }

        [Display(Name = "Tipo de Reporte")]
        public int? IdTipoReporte { get; set; }
        public IEnumerable<SelectListItem>? TiposReporte { get; set; }

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

        [Display(Name = "Nota del Supervisor")]
        [DataType(DataType.MultilineText)]
        public string? NotaSupervisor { get; set; }


        // Detalles
        public List<PreDetPrdPreExpansionViewModel>? PreDetPrdPreExpansions { get; set; }
    }
}
