using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class CatalogoPanelesPch
{
    public int Id { get; set; }

    public string FamiliaProducto { get; set; } = null!;

    public string DescripcionArticulo { get; set; } = null!;

    public string CodigoArticulo { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<DetPrdPaneladoraPch> DetPrdPaneladoraPches { get; set; } = new List<DetPrdPaneladoraPch>();
}
