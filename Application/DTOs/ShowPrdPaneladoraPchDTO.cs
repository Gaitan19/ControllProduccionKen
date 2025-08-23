using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShowPrdPaneladoraPchDTO
    {
        public int Id { get; set; }
        public string Operarios { get; set; } = null!;
        public string Maquina { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string? Observaciones { get; set; }
        public decimal? ProduccionDia { get; set; }
        public decimal? TiempoParo { get; set; }
        public bool AprobadoSupervisor { get; set; }
        public bool AprobadoGerencia { get; set; }
        public string? IdAprobadoSupervisor { get; set; }
        public string? IdAprobadoGerencia { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string IdUsuarioCreacion { get; set; } = null!;
    }
}