using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class DetAlambrePrdCerchaCovintec
{
    public int Id { get; set; }

    public int IdCercha { get; set; }

    public int NumeroAlambre { get; set; }

    public decimal PesoAlambre { get; set; }

    public bool AprobadoSupervisor { get; set; }

    public bool AprobadoGerencia { get; set; }

    public string? IdAprobadoSupervisor { get; set; }

    public string? IdAprobadoGerencia { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual PrdCerchaCovintec IdCerchaNavigation { get; set; } = null!;
}
