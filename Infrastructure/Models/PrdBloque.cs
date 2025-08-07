using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class PrdBloque
{
    public int Id { get; set; }

    public string IdUsuarios { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public bool AprobadoSupervisor { get; set; }

    public bool AprobadoGerencia { get; set; }

    public string? IdAprobadoSupervisor { get; set; }

    public string? IdAprobadoGerencia { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<DetPrdBloque> DetPrdBloques { get; set; } = new List<DetPrdBloque>();
}
