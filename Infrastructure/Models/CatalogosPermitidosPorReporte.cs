using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class CatalogosPermitidosPorReporte
{
    public int Id { get; set; }

    public int IdTipoReporte { get; set; }

    public string Catalogo { get; set; } = null!;

    public int IdCatalogo { get; set; }
}
