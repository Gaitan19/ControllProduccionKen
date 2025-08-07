using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class CatalogoCercha
{
    public int Id { get; set; }

    public int IdLineaProduccion { get; set; }

    public string CodigoArticulo { get; set; } = null!;

    public string DescripcionArticulo { get; set; } = null!;

    public decimal LongitudMetros { get; set; }

    public decimal? EspesorMetros { get; set; }

    public decimal? AreaM2 { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<DetPrdCerchaCovintec> DetPrdCerchaCovintecs { get; set; } = new List<DetPrdCerchaCovintec>();

    public virtual LineaProduccion IdLineaProduccionNavigation { get; set; } = null!;
}
