using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class PreDetPrdPreExpansionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Pre-Expansión")]
        public int PrdpreExpansionId { get; set; }

        // En la UI a veces hay conflictos con el Id de la tabla principal (viewmodel principal),
        // entonces se ocupa este en ese caso para asignar su Id internamente
        public int? PreDetPrdPreExpansionId { get; set; }

        [Required(ErrorMessage = "La Marca/Tipo es obligatoria")]
        [Display(Name = "Marca/Tipo")]
        [StringLength(200, ErrorMessage = "La Marca/Tipo no puede exceder 200 caracteres")]
        public string MarcaTipo { get; set; } = null!;

        [Display(Name = "Código Saco")]
        [StringLength(50, ErrorMessage = "El Código Saco no puede exceder 50 caracteres")]
        public string? CodigoSaco { get; set; }

        [Display(Name = "Lote")]
        [StringLength(50, ErrorMessage = "El Lote no puede exceder 50 caracteres")]
        public string? Lote { get; set; }

        [Display(Name = "Fecha de Producción")]
        [DataType(DataType.Date)]
        public DateTime? FechaProduccion { get; set; }

        // Auditoría
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        // Sub-detalles
        public List<DetPrdPreExpansionViewModel>? DetPrdPreExpansions { get; set; }
    }
}