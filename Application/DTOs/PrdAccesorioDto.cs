using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PrdAccesorioDto
    {
        public int Id { get; set; }
        public List<string> IdUsuarios { get; set; } = new();
        public DateTime Fecha { get; set; }
        public int IdMaquina { get; set; }
        public string? Observaciones { get; set; }
        public bool AprobadoSupervisor { get; set; }
        public bool AprobadoGerencia { get; set; }
        public string? IdAprobadoSupervisor { get; set; }
        public string? IdAprobadoGerencia { get; set; }
        public string IdUsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public decimal? MermaMallaCovintecKg { get; set; }
        public decimal? MermaMallaPchKg { get; set; }
        public decimal? MermaBobinasKg { get; set; }
        public decimal? MermaLitewallKg { get; set; }
        
        [Display(Name = "Tiempo Paro (Horas)")]
        [Range(0, double.MaxValue, ErrorMessage = "El tiempo de paro debe ser mayor o igual a 0")]
        public decimal? TiempoParo { get; set; }

        [Display(Name = "Nota Supervisor")]
        public string? NotaSupervisor { get; set; }

        public List<DetPrdAccesorioDto>? DetPrdAccesorios { get; set; }
    }
}