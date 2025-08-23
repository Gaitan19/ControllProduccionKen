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

        [Display(Name = "Lote")]
        public string? Lote { get; set; }

        [Display(Name = "Fecha Producción")]
        [DataType(DataType.Date)]
        public DateTime? FechaProduccion { get; set; }

        [Display(Name = "Código de Saco")]
        public string? CodigoSaco { get; set; }

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


        // Detalles
        public List<DetPrdPreExpansionViewModel>? DetPrdPreExpansions { get; set; }
    }

    public class DetPrdPreExpansionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Pre-Expansión")]
        public int PrdpreExpansionId { get; set; }

        // En la UI a veces hay conflictos con el Id de la tabla principal (viewmodel principal),
        // entonces se ocupa este en ese caso para asignar su Id internamente
        public int? DetPrdPreExpansionId { get; set; }

        [Required(ErrorMessage = "La Hora es obligatoria")]
        [Display(Name = "Hora")]
        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; }

        [Required(ErrorMessage = "El No. de Batch es obligatorio")]
        [Display(Name = "No. Batch")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public int NoBatch { get; set; }

        [Required(ErrorMessage = "La Densidad Esperada es obligatoria")]
        [Display(Name = "Densidad Esperada")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public int DensidadEsperada { get; set; }

        [Required(ErrorMessage = "El Peso del Batch (g) es obligatorio")]
        [Display(Name = "Peso Batch (g)")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public decimal PesoBatchGr { get; set; }

        [Required(ErrorMessage = "La Densidad es obligatoria")]
        [Display(Name = "Densidad")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public decimal Densidad { get; set; }

        [Required(ErrorMessage = "Los Kg por Batch son obligatorios")]
        [Display(Name = "Kg por Batch")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public int KgPorBatch { get; set; }

        [Required(ErrorMessage = "La Presión (psi) es obligatoria")]
        [Display(Name = "Presión (psi)")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public int PresionPsi { get; set; }

        [Required(ErrorMessage = "El Tiempo del Batch (seg) es obligatorio")]
        [Display(Name = "Tiempo Batch (seg)")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public int TiempoBatchSeg { get; set; }

        [Required(ErrorMessage = "La Temperatura (°C) es obligatoria")]
        [Display(Name = "Temperatura (°C)")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public int TemperaturaC { get; set; }

        [Required(ErrorMessage = "El Silo es obligatorio")]
        [Display(Name = "Silo")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public int Silo { get; set; }
        [Display(Name = "Paso")]
        [Range(1, 2, ErrorMessage = "Debe ser un número positivo")]
        public int? Paso { get; set; } = 1;

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
