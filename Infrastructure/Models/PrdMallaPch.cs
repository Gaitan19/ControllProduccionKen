using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class PrdMallaPch
{
    public int Id { get; set; }

    public string IdUsuarios { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string? Observaciones { get; set; }

    public decimal? ProduccionDia { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public bool AprobadoSupervisor { get; set; }

    public bool AprobadoGerencia { get; set; }

    public string? IdAprobadoSupervisor { get; set; }

    public string? IdAprobadoGerencia { get; set; }

    public decimal? TiempoParo { get; set; }

    public string? NotaSupervisor { get; set; }

    public virtual ICollection<DetPrdPchMaquinaA> DetPrdPchMaquinaAs { get; set; } = new List<DetPrdPchMaquinaA>();

    public virtual ICollection<DetPrdPchMaquinaB> DetPrdPchMaquinaBs { get; set; } = new List<DetPrdPchMaquinaB>();

    public virtual ICollection<DetPrdPchMaquinaC> DetPrdPchMaquinaCs { get; set; } = new List<DetPrdPchMaquinaC>();
}
