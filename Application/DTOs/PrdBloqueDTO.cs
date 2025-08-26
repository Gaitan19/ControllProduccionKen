using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PrdBloqueDTO
    {
        public int Id { get; set; }

        public List<string> IdUsuarios { get; set; } = null!;

        public DateTime Fecha { get; set; }

        public bool AprobadoSupervisor { get; set; }

        public bool AprobadoGerencia { get; set; }

        public string? IdAprobadoSupervisor { get; set; }

        public string? IdAprobadoGerencia { get; set; }

        public string IdUsuarioCreacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public decimal? TiempoParo { get; set; }

        [Display(Name = "Nota del Supervisor")]
        [StringLength(1000, ErrorMessage = "La nota del supervisor no puede exceder 1000 caracteres")]
        public string? NotaSupervisor { get; set; }

        public List<DetPrdBloqueDTO>? DetPrdBloques { get; set; }
    }
}