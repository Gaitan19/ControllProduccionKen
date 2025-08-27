using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class PreDetPrdpreExpansion
{
    public int Id { get; set; }

    public string MarcaTipo { get; set; } = null!;

    public string? CodigoSaco { get; set; }

    public string? Lote { get; set; }

    public DateTime? FechaProduccion { get; set; }

    public int PrdpreExpansionId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<DetPrdpreExpansion> DetPrdpreExpansions { get; set; } = new List<DetPrdpreExpansion>();

    public virtual PrdpreExpansion PrdpreExpansion { get; set; } = null!;
}
