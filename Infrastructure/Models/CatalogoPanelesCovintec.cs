using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class CatalogoPanelesCovintec
{
    public int Id { get; set; }

    public int IdLineaProduccion { get; set; }

    public string DescripcionArticulo { get; set; } = null!;

    public string CodigoArticulo { get; set; } = null!;

    public decimal Mts2PorPanel { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<DetPrdMallaCovintec> DetPrdMallaCovintecs { get; set; } = new List<DetPrdMallaCovintec>();

    public virtual ICollection<DetPrdPanelesCovintec> DetPrdPanelesCovintecs { get; set; } = new List<DetPrdPanelesCovintec>();

    public virtual LineaProduccion IdLineaProduccionNavigation { get; set; } = null!;
}
