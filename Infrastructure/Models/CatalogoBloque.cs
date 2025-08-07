using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class CatalogoBloque
{
    public int Id { get; set; }

    public string? Bloque { get; set; }

    public decimal? CubicajeM3 { get; set; }

    public string? Medidas { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<DetPrdBloque> DetPrdBloques { get; set; } = new List<DetPrdBloque>();

    public virtual ICollection<SubDetPrdBloque> SubDetPrdBloques { get; set; } = new List<SubDetPrdBloque>();
}
