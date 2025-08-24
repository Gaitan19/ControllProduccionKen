using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class PrdCerchaCovintecViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tipo de Reporte")]
        public int? IdTipoReporte { get; set; }

        [Required(ErrorMessage = "El campo Operador es obligatorio")]
        [Display(Name = "Operador")]
        public List<string> IdUsuarios { get; set; } = new List<string>();

        public IEnumerable<SelectListItem>? Operarios { get; set; }

        [Required(ErrorMessage = "El campo Máquina es obligatorio")]
        [Display(Name = "Máquina")]
        public int IdMaquina { get; set; }

        public IEnumerable<SelectListItem>? Maquinas { get; set; }

        [Required(ErrorMessage = "El campo Conteo Inicial es obligatorio")]
        [Display(Name = "Conteo Inicial")]
        public int ConteoInicial { get; set; }

        [Required(ErrorMessage = "El campo Conteo Final es obligatorio")]
        [Display(Name = "Conteo Final")]
        public int ConteoFinal { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Display(Name = "Merma Alambre (Kg)")]
        public decimal? MermaAlambre { get; set; }

        [Display(Name = "Tiempo Paro (Horas)")]
        public decimal? TiempoParo { get; set; }

        [Display(Name = "Articulo")]
        public IEnumerable<SelectListItem>? CatalogoCercha { get; set; }
        [Display(Name = "Tipo de Fabricacion")]
        public IEnumerable<SelectListItem>? TiposFabricacion { get; set; }

        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string? Observaciones { get; set; }

        // Campos de auditoría – se rellenan internamente
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public bool? AprobadoSupervisor { get; set; }
        public bool? AprobadoGerencia { get; set; }

        // Detalles
        public List<DetAlambrePrdCerchaCovintecViewModel>? DetAlambrePrdCerchaCovintec { get; set; }
        public List<DetPrdCerchaCovintecViewModel>? DetPrdCerchaCovintec { get; set; }
    }

    public class DetPrdCerchaCovintecViewModel
    {
        public int Id { get; set; }
        public int? detrpdId { get; set; }

        public int? IdCercha { get; set; }

        public int IdArticulo { get; set; }
        public string? Articulo { get; set; }

        public int CantidadProducida { get; set; }

        public int CantidadNoConforme { get; set; }

        public int IdTipoFabricacion { get; set; }
        public string? TipoFabricacion { get; set; }

        public int? NumeroPedido { get; set; }

        public decimal ProduccionDia { get; set; }

        public bool AprobadoSupervisor { get; set; }

        public bool AprobadoGerencia { get; set; }

        public string? IdAprobadoSupervisor { get; set; }

        public string? IdAprobadoGerencia { get; set; }

        public string? IdUsuarioCreacion { get; set; } = null!;

        public DateTime? FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }

    public class DetAlambrePrdCerchaCovintecViewModel
    {

        public int? Id { get; set; }
        public int? detAlambreId { get; set; }

        public int? IdCercha { get; set; }

        public int NumeroAlambre { get; set; }

        public decimal PesoAlambre { get; set; }

        public bool AprobadoSupervisor { get; set; }

        public bool AprobadoGerencia { get; set; }

        public string? IdAprobadoSupervisor { get; set; }

        public string? IdAprobadoGerencia { get; set; }

        public string? IdUsuarioCreacion { get; set; } = null!;

        public DateTime? FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
