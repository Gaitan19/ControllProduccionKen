using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlProduccion.ViewModel
{
    public class PrdAccesorioViewModel
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

        // Catálogos para dropdowns
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

        [Display(Name = "Nota Supervisor")]
        [DataType(DataType.MultilineText)]
        public string? NotaSupervisor { get; set; }

        // Catálogos para dropdowns
        [Display(Name = "Tipo de Artículo")]
        public IEnumerable<SelectListItem>? TiposArticulo { get; set; }

        [Display(Name = "Artículo")]
        public IEnumerable<SelectListItem>? Articulos { get; set; }

        [Display(Name = "Tipo de Fabricacion")]
        public IEnumerable<SelectListItem>? TiposFabricacion { get; set; }

        [Display(Name = "Malla Covintec")]
        public IEnumerable<SelectListItem>? MallasCovintec { get; set; }

        [Display(Name = "Tipo de Malla PCH")]
        public IEnumerable<SelectListItem>? TiposMallaPch { get; set; }

        [Display(Name = "Ancho Bobina")]
        public IEnumerable<SelectListItem>? AnchosBobina { get; set; }

        [Display(Name = "Calibre")]
        public IEnumerable<SelectListItem>? Calibres { get; set; }

        [Display(Name = "Observaciones")]
        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder 500 caracteres")]
        public string? Observaciones { get; set; }

        // Campos de merma
        [Display(Name = "Merma Malla Covintec (Kg)")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public decimal? MermaMallaCovintecKg { get; set; }

        [Display(Name = "Merma Malla PCH (Kg)")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public decimal? MermaMallaPchKg { get; set; }

        [Display(Name = "Merma Bobinas (Kg)")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public decimal? MermaBobinasKg { get; set; }

        [Display(Name = "Merma Litewall (Kg)")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe ser un número positivo")]
        public decimal? MermaLitewallKg { get; set; }

        [Display(Name = "Tiempo Paro (Horas)")]
        [Range(0, double.MaxValue, ErrorMessage = "El tiempo de paro debe ser mayor o igual a 0")]
        public decimal? TiempoParo { get; set; }

        // Detalles
        public List<DetPrdAccesorioViewModel>? DetPrdAccesorios { get; set; }

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}