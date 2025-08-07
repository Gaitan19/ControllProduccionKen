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

        // Detalles
        public List<DetPrdBloqueViewModel>? DetPrdBloques { get; set; }

        // Validación cruzada
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    // Validar que la fecha no sea futura
        //    if (Fecha > DateTime.Now.Date)
        //        yield return new ValidationResult(
        //            "La fecha no puede ser futura.",
        //            new[] { nameof(Fecha) });

        //    // Validar que se haya seleccionado al menos un operario
        //    if (IdUsuarios == null || !IdUsuarios.Any())
        //        yield return new ValidationResult(
        //            "Debe seleccionar al menos un operario.",
        //            new[] { nameof(IdUsuarios) });

        //    // Validar detalles si existen
        //    if (DetPrdBloques != null && DetPrdBloques.Any())
        //    {
        //        for (int i = 0; i < DetPrdBloques.Count; i++)
        //        {
        //            var detalle = DetPrdBloques[i];
                    
        //            // Validar que cada detalle tenga al menos un sub-detalle
        //            if (detalle.SubDetPrdBloques == null || !detalle.SubDetPrdBloques.Any())
        //                yield return new ValidationResult(
        //                    $"El detalle en la posición {i + 1} debe tener al menos un sub-detalle.",
        //                    new[] { $"DetPrdBloques[{i}].SubDetPrdBloques" });
        //        }
        //    }
        //}
    }
}