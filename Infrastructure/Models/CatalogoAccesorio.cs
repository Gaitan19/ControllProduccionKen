using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class CatalogoAccesorio
{
    public int Id { get; set; }

    public string FamiliaProductos { get; set; } = null!;

    public string DescripcionArticulo { get; set; } = null!;

    public string CodigoArticulo { get; set; } = null!;

    public int IdTipo { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<DetPrdAccesorio> DetPrdAccesorios { get; set; } = new List<DetPrdAccesorio>();

    public virtual MaestroCatalogo IdTipoNavigation { get; set; } = null!;
}
