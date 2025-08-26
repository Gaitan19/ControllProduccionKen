using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class PrdBloqueViewModel 
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

        [Display(Name = "Tiempo Paro (Horas)")]
        [Range(0, double.MaxValue, ErrorMessage = "El tiempo de paro debe ser mayor o igual a 0")]
        public decimal? TiempoParo { get; set; }

        [Display(Name = "Aprobado por Supervisor")]
        public bool? AprobadoSupervisor { get; set; }
        public string? IdAprobadoSupervisor { get; set; }

        [Display(Name = "Aprobado por Gerencia")]
        public bool? AprobadoGerencia { get; set; }
        public string? IdAprobadoGerencia { get; set; }

        // Catálogos para dropdowns
        [Display(Name = "Máquina")]
        public IEnumerable<SelectListItem>? Maquinas { get; set; }

        [Display(Name = "Artículo")]
        public IEnumerable<SelectListItem>? Articulos { get; set; }

        [Display(Name = "Tipo de Fabricación")]
        public IEnumerable<SelectListItem>? TiposFabricacion { get; set; }

        [Display(Name = "Catálogo de Bloques")]
        public IEnumerable<SelectListItem>? CatalogoBloques { get; set; }

        [Display(Name = "Densidad")]
        public IEnumerable<SelectListItem>? CatalogoDensidad { get; set; }

        [Display(Name = "Tipo de Bloque")]
        public IEnumerable<SelectListItem>? CatalogoTipoBloque { get; set; }

        [Display(Name = "Nota del Supervisor")]
        [StringLength(1000, ErrorMessage = "La nota del supervisor no puede exceder 1000 caracteres")]
        public string? NotaSupervisor { get; set; }

        // Detalles
        public List<DetPrdBloqueViewModel>? DetPrdBloques { get; set; }

  
    }
}