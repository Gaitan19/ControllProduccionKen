using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PrdOtroDto
    {
        public int Id { get; set; }
        public List<string> IdUsuarios { get; set; } = new();
        public DateTime Fecha { get; set; }
        public string IdUsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public bool AprobadoSupervisor { get; set; }
        public bool AprobadoGerencia { get; set; }
        public string? IdAprobadoSupervisor { get; set; }
        public string? IdAprobadoGerencia { get; set; }

        public int? IdTipoReporte { get; set; }
        public decimal? TiempoParo { get; set; }

        [Display(Name = "Nota Supervisor")]
        public string? NotaSupervisor { get; set; }

        public List<DetPrdOtroDTO>? DetPrdOtros { get; set; }
    }
}