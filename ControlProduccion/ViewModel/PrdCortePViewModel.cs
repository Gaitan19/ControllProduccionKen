using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlProduccion.ViewModel
{
    public class PrdCortePViewModel
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

        // Cat�logos para dropdowns
        [Display(Name = "Maquinas")]
        public IEnumerable<SelectListItem>? Maquinas { get; set; }
        [Required(ErrorMessage = "El campo Maquina es obligatorio")]
        [Display(Name = "Maquina")]
        public int IdMaquina { get; set; }
        public string? Maquina { get; set; }

        [Display(Name = "Aprobado por Supervisor")]
        public bool? AprobadoSupervisor { get; set; }
        public string? IdAprobadoSupervisor { get; set; }

        [Display(Name = "Aprobado por Gerencia")]
        public bool? AprobadoGerencia { get; set; }
        public string? IdAprobadoGerencia { get; set; }

        // Cat�logos para dropdowns
        [Display(Name = "Articulo")]
        public IEnumerable<SelectListItem>? Articulos { get; set; }

        [Display(Name = "Tipo de Fabricacion")]
        public IEnumerable<SelectListItem>? TiposFabricacion { get; set; }

        [Display(Name = "Densidad")]
        public IEnumerable<SelectListItem>? CatalogoDensidad { get; set; }

        [Display(Name = "Tipo de Bloque")]
        public IEnumerable<SelectListItem>? CatalogoTipoBloque { get; set; }

        [Display(Name = "Codigo de bloque")]
        public IEnumerable<SelectListItem>? subDetalleBloqueCodigo { get; set; }

        [Display(Name = "Observaciones")]
        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder 500 caracteres")]
        public string? Observaciones { get; set; }

        [Display(Name = "Tiempo de paro (horas)")]
        [Range(0, double.MaxValue, ErrorMessage = "El tiempo de paro debe ser mayor o igual a 0")]
        public decimal? TiempoParo { get; set; }

        [Display(Name = "Nota Supervisor")]
        public string? NotaSupervisor { get; set; }

        // Detalles
        public List<DetPrdCortePViewModel>? DetPrdCorteP { get; set; }

        // Auditor�a
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}