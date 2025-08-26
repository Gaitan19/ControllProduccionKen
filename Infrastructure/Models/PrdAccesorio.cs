using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class PrdAccesorio
{
    public int Id { get; set; }

    public string IdUsuarios { get; set; } = null!;

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

    public decimal? TiempoParo { get; set; }

    public string? NotaSupervisor { get; set; }

    public virtual Maquina IdMaquinaNavigation { get; set; } = null!;
}
