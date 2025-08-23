using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class CatalogoMallasCovintec
{
    public int Id { get; set; }

    public int IdLineaProduccion { get; set; }

    public string CodigoArticulo { get; set; } = null!;

    public string DescripcionArticulo { get; set; } = null!;

    public decimal LongitudCentimetros { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<DetPrdAccesorio> DetPrdAccesorios { get; set; } = new List<DetPrdAccesorio>();

    public virtual LineaProduccion IdLineaProduccionNavigation { get; set; } = null!;
}
