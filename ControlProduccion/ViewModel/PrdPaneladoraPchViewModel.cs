using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlProduccion.ViewModel
{
    public class PrdPaneladoraPchViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Operadores es obligatorio")]
        [Display(Name = "Operadores")]
        public List<string> IdUsuarios { get; set; } = new();

        public IEnumerable<SelectListItem>? Operarios { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El campo Máquina es obligatorio")]
        [Display(Name = "Máquina")]
        public int IdMaquina { get; set; }

        [Display(Name = "Máquinas")]
        public IEnumerable<SelectListItem>? Maquinas { get; set; }

        [Display(Name = "Observaciones")]
        [StringLength(4000, ErrorMessage = "Las observaciones no pueden exceder 4000 caracteres")]
        public string? Observaciones { get; set; }

        [Display(Name = "Producción del día (Mts²)")]
        [Range(0, double.MaxValue, ErrorMessage = "La producción debe ser mayor o igual a 0")]
        public decimal? ProduccionDia { get; set; }

        [Display(Name = "Tiempo de paro (horas)")]
        [Range(0, double.MaxValue, ErrorMessage = "El tiempo de paro debe ser mayor o igual a 0")]
        public decimal? TiempoParo { get; set; }

        // Aprobaciones
        [Display(Name = "Aprobado por Supervisor")]
        public bool? AprobadoSupervisor { get; set; }
        public string? IdAprobadoSupervisor { get; set; }

        [Display(Name = "Aprobado por Gerencia")]
        public bool? AprobadoGerencia { get; set; }
        public string? IdAprobadoGerencia { get; set; }

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        // Catálogos para dropdowns
        [Display(Name = "Tipos de Fabricación")]
        public IEnumerable<SelectListItem>? TiposFabricacion { get; set; }

        [Display(Name = "Paneles")]
        public IEnumerable<SelectListItem>? CatalogoPaneles { get; set; }

        // Detalles
        public List<DetPrdPaneladoraPchViewModel>? DetPrdPaneladoraPch { get; set; }
    }

    public class DetPrdPaneladoraPchViewModel
    {
        [Key]
        public int DetPrdPaneladoraPchId { get; set; }

        public int PrdPaneladoraPchId { get; set; }

        [Required(ErrorMessage = "El campo Artículo es obligatorio")]
        [Display(Name = "Artículo")]
        public int IdArticulo { get; set; }

        public string? Articulo { get; set; }

        [Required(ErrorMessage = "El campo Longitud es obligatorio")]
        [Display(Name = "Longitud (mts)")]
        [Range(0.1, double.MaxValue, ErrorMessage = "La longitud debe ser mayor a 0")]
        public decimal Longitud { get; set; }

        [Required(ErrorMessage = "El campo Cantidad Producida es obligatorio")]
        [Display(Name = "Cantidad Producida")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad producida debe ser mayor o igual a 0")]
        public int CantidadProducida { get; set; }

        [Required(ErrorMessage = "El campo Cantidad No Conforme es obligatorio")]
        [Display(Name = "Cantidad No Conforme")]
        [Range(0, int.MaxValue, ErrorMessage = "La cantidad no conforme debe ser mayor o igual a 0")]
        public int CantidadNoConforme { get; set; }


        [Display(Name = "Mts² por Panel")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Los metros cuadrados por panel deben ser mayor a 0")]
        public decimal? Mts2PorPanel { get; set; }

        [Required(ErrorMessage = "El campo Tipo de Fabricación es obligatorio")]
        [Display(Name = "Tipo de Fabricación")]
        public int IdTipoFabricacion { get; set; }

        public string? TipoFabricacion { get; set; }

        [Display(Name = "Número de Pedido")]
        public int? NumeroPedido { get; set; }

        [Required(ErrorMessage = "El campo Número de Alambre es obligatorio")]
        [Display(Name = "Número de Alambre")]
        [Range(1, int.MaxValue, ErrorMessage = "El número de alambre debe ser mayor a 0")]
        public int NumeroAlambre { get; set; }

        [Required(ErrorMessage = "El campo Peso del Alambre es obligatorio")]
        [Display(Name = "Peso del Alambre (Kg)")]
        [Range(0.1, double.MaxValue, ErrorMessage = "El peso del alambre debe ser mayor a 0")]
        public decimal PesoAlambreKg { get; set; }

        [Required(ErrorMessage = "El campo Merma del Alambre es obligatorio")]
        [Display(Name = "Merma del Alambre (Kg)")]
        [Range(0, double.MaxValue, ErrorMessage = "La merma del alambre debe ser mayor o igual a 0")]
        public decimal MermaAlambreKg { get; set; }

        // Catálogos para dropdowns
        public IEnumerable<SelectListItem>? TiposFabricacion { get; set; }
        public IEnumerable<SelectListItem>? CatalogoPaneles { get; set; }
    }
}