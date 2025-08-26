using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class PrdNevera
{
    public int Id { get; set; }

    public decimal? TiempoParo { get; set; }

    public int? IdTipoReporte { get; set; }

    public string IdUsuarios { get; set; } = null!;

    public int IdMaquina { get; set; }

    public DateTime Fecha { get; set; }

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFin { get; set; }

    public string? Observaciones { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public bool AprobadoSupervisor { get; set; }

    public bool AprobadoGerencia { get; set; }

    public string? IdAprobadoSupervisor { get; set; }

    public string? IdAprobadoGerencia { get; set; }

    public string? NotaSupervisor { get; set; }

    public virtual ICollection<DetPrdNevera> DetPrdNeveras { get; set; } = new List<DetPrdNevera>();

    public virtual Maquina IdMaquinaNavigation { get; set; } = null!;
}
