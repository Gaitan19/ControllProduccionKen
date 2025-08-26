using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class PrdCerchaCovintec
{
    public int Id { get; set; }

    public decimal? TiempoParo { get; set; }

    public int? IdTipoReporte { get; set; }

    public string IdUsuarios { get; set; } = null!;

    public int IdMaquina { get; set; }

    public int ConteoInicial { get; set; }

    public int ConteoFinal { get; set; }

    public DateTime Fecha { get; set; }

    public decimal? MermaAlambre { get; set; }

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

    public virtual ICollection<DetAlambrePrdCerchaCovintec> DetAlambrePrdCerchaCovintecs { get; set; } = new List<DetAlambrePrdCerchaCovintec>();

    public virtual ICollection<DetPrdCerchaCovintec> DetPrdCerchaCovintecs { get; set; } = new List<DetPrdCerchaCovintec>();

    public virtual Maquina IdMaquinaNavigation { get; set; } = null!;
}
