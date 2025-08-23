using System;

namespace Application.DTOs
{
    public class ShowPrdPreExpansionDto
    {
        public int Id { get; set; }
        public int? IdTipoReporte { get; set; }
        public string Operarios { get; set; }
        public string Maquina { get; set; }
        public DateTime? Fecha { get; set; }
        public bool AprobadoSupervisor { get; set; }
        public bool AprobadoGerencia { get; set; }
        public string? IdAprobadoSupervisor { get; set; }
        public string? IdAprobadoGerencia { get; set; }
    }
}