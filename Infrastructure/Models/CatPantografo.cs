using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class CatPantografo
{
    public int Id { get; set; }

    public int? IdLineaProduccion { get; set; }

    public string CodigoArticulo { get; set; } = null!;

    public string DescripcionArticulo { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<DetPrdCorteP> DetPrdCortePs { get; set; } = new List<DetPrdCorteP>();
}
