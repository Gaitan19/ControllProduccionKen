using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShowPrdAccesorioDto
    {
        public int Id { get; set; }
        public string Operarios { get; set; } = string.Empty;
        public string Maquina { get; set; } = string.Empty;
        public DateTime? Fecha { get; set; }
        public bool AprobadoSupervisor { get; set; }
        public bool AprobadoGerencia { get; set; }
        public string? IdAprobadoSupervisor { get; set; }
        public string? IdAprobadoGerencia { get; set; }
    }
}