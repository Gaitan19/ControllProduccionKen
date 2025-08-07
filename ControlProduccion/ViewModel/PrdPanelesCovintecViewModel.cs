using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class PrdPanelesCovintecViewModel
    {
        [Key]
        public int Id { get; set; }
      
        [Display(Name = "Tipo de Reporte")]
        public int? IdTipoReporte { get; set; }

        [Required(ErrorMessage = "El campo Operador  es obligatorio")]
        [Display(Name = "Operador")]
        public List<string> IdUsuarios { get; set; }
        public IEnumerable<SelectListItem>? Operarios { get; set; }
        [Required(ErrorMessage = "El campo Máquina es obligatorio")]
        [Display(Name = "Máquina")]
        public int IdMaquina { get; set; }
        // Lista desplegable para máquinas
        public IEnumerable<SelectListItem>? Maquinas { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;
     

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Merma Alambre")]
        public decimal MermaAlambre { get; set; }

        [Display(Name = "Tiempo Paro (Horas)")]
        public decimal? TiempoParo { get; set; }

        [Display(Name = "Articulo")]
        public IEnumerable<SelectListItem>? CatalogoPaneles { get; set; }

        [Display(Name = "Tipo de Fabricacion")]
        public IEnumerable<SelectListItem>? TiposFabricacion { get; set; }
        // Estos campos pueden ser asignados automáticamente o manejarse de forma interna
        public string? IdUsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string? IdAprobadoSupervisor { get; set; }
        public string? IdAprobadoGerencia { get; set; }
        public bool? AprobadoSupervisor { get; set; }
        public bool? AprobadoGerencia { get; set; }
        public List<DetPrdPanelesCovintecViewModel>? DetPrdPanelesCovintec { get; set; }
        public List<DetAlambrePrdPanelesCovintecViewModel>? DetAlambrePrdPanelesCovintec { get; set; }
    }


    public class DetPrdPanelesCovintecViewModel
    {
       
        public int Id { get; set; }
        public int? detrpdId { get; set; }
        public int IdPanel { get; set; }
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
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }

    public class DetAlambrePrdPanelesCovintecViewModel
    {
       
        public int? Id { get; set; }
        public int? detAlambreId { get; set; }
        public int? IdPanel { get; set; }
        public int NumeroAlambre { get; set; }
        public decimal PesoAlambre { get; set; }
      
        public string? IdUsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public bool AprobadoSupervisor { get; set; }
        public bool AprobadoGerencia { get; set; }
        public string? IdAprobadoSupervisor { get; set; }
        public string? IdAprobadoGerencia { get; set; }
    }
}
