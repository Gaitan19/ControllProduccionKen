using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlProduccion.ViewModel
{
    public class PrdMallaPchViewModel
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

        [Display(Name = "Observaciones")]
        [StringLength(500, ErrorMessage = "Las observaciones no pueden exceder 500 caracteres")]
        public string? Observaciones { get; set; }

        [Display(Name = "Tiempo Paro (Horas)")]
        [Range(0, double.MaxValue, ErrorMessage = "El tiempo de paro debe ser mayor o igual a 0")]
        public decimal? TiempoParo { get; set; }

        //para la pantalla create no va este campo. para las demas si
        [Display(Name = "Producción del día (Mts)")]
        [Range(0, double.MaxValue)]
        public decimal? ProduccionTotal { get; set; }

        [Display(Name = "Nota del Supervisor")]
        [StringLength(1000, ErrorMessage = "La nota del supervisor no puede exceder 1000 caracteres")]
        public string? NotaSupervisor { get; set; }

        // Aprobaciones (nulas en VM; en DTO serán false si null)
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

        // Catálogos comunes para reutilizar en los detalles
        [Display(Name = "Máquinas")]
        public IEnumerable<SelectListItem>? Maquinas { get; set; }

        [Display(Name = "Tipos de Fabricación")]
        public IEnumerable<SelectListItem>? TiposFabricacion { get; set; }

        [Display(Name = "Tipos de Malla")]
        public IEnumerable<SelectListItem>? TiposMalla { get; set; }

        // Detalles
        public List<DetPrdPchMaquinaAViewModel>? DetPrdPchMaquinaAs { get; set; }
        public List<DetPrdPchMaquinaBViewModel>? DetPrdPchMaquinaBs { get; set; }
        public List<DetPrdPchMaquinaCViewModel>? DetPrdPchMaquinaCs { get; set; }
    }

    public class DetPrdPchMaquinaAViewModel
    {
        public int Id { get; set; }
        public int PrdMallaPchId { get; set; }
        //como ya el viewmodel principal (PrdMallaPchViewModel) tiene campo Id, usamos un campo diferente para poder usarlo
        //en la vsta y no tener problemas de duplicados o conflictos. entonces el DetPrdPchMaquinaAId equivale al Id de DetPrdPchMaquinaAViewModel, esto mas que todo para la pantalla Edit
        public int? DetPrdPchMaquinaAId { get; set; }

        [Required(ErrorMessage = "La Máquina es obligatoria")]
        [Display(Name = "Máquina")]
        public int IdMaquina { get; set; }
        public IEnumerable<SelectListItem>? Maquinas { get; set; }

        [Required]
        [Display(Name = "Hilos Transversales (un)")]
        [Range(0, int.MaxValue)]
        public int HilosTransversalesUn { get; set; }

        [Required]
        [Display(Name = "Merma Hilos Transversales (kg)")]
        [Range(0, double.MaxValue)]
        public decimal MermaHilosTransversalesKg { get; set; }

        [Required]
        [Display(Name = "Tipo de Fabricación")]
        public int IdTipoFabricacion { get; set; }
      

        [Display(Name = "Número de Pedido")]
        public int? NumeroPedido { get; set; }

        [Required]
        [Display(Name = "Longitud (m)")]
        [Range(0, double.MaxValue)]
        public decimal Longitud { get; set; }

        [Required]
        [Display(Name = "Cantidad (un)")]
        [Range(0, int.MaxValue)]
        public int Cantidad { get; set; }

        //para la pantalla create no va este campo. para las demas si
        [Display(Name = "Producción (Mts)")]
        [Range(0, double.MaxValue)]
        public decimal? Produccion { get; set; }

        [Required]
        [Display(Name = "N.º Alambre A")]
        [Range(0, int.MaxValue)]
        public int NumeroAlambreA { get; set; }

        [Required]
        [Display(Name = "Peso Alambre A (kg)")]
        [Range(0, double.MaxValue)]
        public decimal PesoAlambreKgA { get; set; }

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }

    public class DetPrdPchMaquinaBViewModel
    {
        public int Id { get; set; }
        public int PrdMallaPchId { get; set; }
        //como ya el viewmodel principal (PrdMallaPchViewModel) tiene campo Id, usamos un campo diferente para poder usarlo
        //en la vsta y no tener problemas de duplicados o conflictos. entonces el DetPrdPchMaquinaBId equivale al Id de DetPrdPchMaquinaBViewModel, esto mas que todo para la pantalla Edit
        public int? DetPrdPchMaquinaBId { get; set; }

        [Required(ErrorMessage = "La Máquina es obligatoria")]
        [Display(Name = "Máquina")]
        public int IdMaquina { get; set; }
        public IEnumerable<SelectListItem>? Maquinas { get; set; }


        [Required]
        [Display(Name = "Hilos Longitudinales (un)")]
        [Range(0, int.MaxValue)]
        public int HilosLongitudinalesUn { get; set; }

        [Required]
        [Display(Name = "Merma Hilos Longitudinales (kg)")]
        [Range(0, double.MaxValue)]
        public decimal MermaHilosLongitudinalesKg { get; set; }

        [Required]
        [Display(Name = "Tipo de Fabricación")]
        public int IdTipoFabricacion { get; set; }
     

        [Display(Name = "Número de Pedido")]
        public int? NumeroPedido { get; set; }

        [Required]
        [Display(Name = "Longitud (m)")]
        [Range(0, double.MaxValue)]
        public decimal Longitud { get; set; }

        [Required]
        [Display(Name = "Cantidad (un)")]
        [Range(0, int.MaxValue)]
        public int Cantidad { get; set; }
        //para la pantalla create no va este campo. para las demas si
        [Display(Name = "Producción (Mts)")]
        [Range(0, double.MaxValue)]
        public decimal? Produccion { get; set; }

        [Required]
        [Display(Name = "N.º Alambre B")]
        [Range(0, int.MaxValue)]
        public int NumeroAlambreB { get; set; }

        [Required]
        [Display(Name = "Peso Alambre B (kg)")]
        [Range(0, double.MaxValue)]
        public decimal PesoAlambreKgB { get; set; }

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }

    public class DetPrdPchMaquinaCViewModel
    {
        public int Id { get; set; }
        public int PrdMallaPchId { get; set; }
        //como ya el viewmodel principal (PrdMallaPchViewModel) tiene campo Id, usamos un campo diferente para poder usarlo
        //en la vsta y no tener problemas de duplicados o conflictos. entonces el DetPrdPchMaquinaCId equivale al Id de DetPrdPchMaquinaCViewModel, esto mas que todo para la pantalla Edit
        public int? DetPrdPchMaquinaCId { get; set; }

        [Required(ErrorMessage = "La Máquina es obligatoria")]
        [Display(Name = "Máquina")]
        public int IdMaquina { get; set; }
        public IEnumerable<SelectListItem>? Maquinas { get; set; }

        [Required]
        [Display(Name = "Tipo de Malla")]
        public int IdTipoMalla { get; set; }

        [Required]
        [Display(Name = "Merma de Mallas (kg)")]
        [Range(0, double.MaxValue)]
        public decimal MermaMallasKg { get; set; }

        [Required]
        [Display(Name = "Tipo de Fabricación")]
        public int IdTipoFabricacion { get; set; }
    

        [Display(Name = "Número de Pedido")]
        public int? NumeroPedido { get; set; }

        [Required]
        [Display(Name = "Longitud (m)")]
        [Range(0, double.MaxValue)]
        public decimal Longitud { get; set; }

        [Required]
        [Display(Name = "Cantidad (un)")]
        [Range(0, int.MaxValue)]
        public int Cantidad { get; set; }

        //para la pantalla create no va este campo. para las demas si
        [Display(Name = "Producción (Mts)")]
        [Range(0, double.MaxValue)]
        public decimal? Produccion { get; set; }

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
