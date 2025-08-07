using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Reporte
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? TablaReporte { get; set; }

    public int IdLineaProduccion { get; set; }

    public bool? Activo { get; set; }

    public virtual LineaProduccion IdLineaProduccionNavigation { get; set; } = null!;

    public virtual ICollection<PrdMallaCovintec> PrdMallaCovintecs { get; set; } = new List<PrdMallaCovintec>();

    public virtual ICollection<PrdPanelesCovintec> PrdPanelesCovintecs { get; set; } = new List<PrdPanelesCovintec>();
}
