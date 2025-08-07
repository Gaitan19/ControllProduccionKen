using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class DetAlambrePrdMallaCovintec
{
    public int Id { get; set; }

    public int IdMalla { get; set; }

    public int NumeroAlambre { get; set; }

    public decimal PesoAlambre { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public bool AprobadoSupervisor { get; set; }

    public bool AprobadoGerencia { get; set; }

    public string? IdAprobadoSupervisor { get; set; }

    public string? IdAprobadoGerencia { get; set; }

    public virtual PrdMallaCovintec IdMallaNavigation { get; set; } = null!;
}
