using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class CatTipoMalla
{
    public int Id { get; set; }

    public string Cuadricula { get; set; } = null!;

    public string? PesoPorMts2 { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<DetPrdAccesorio> DetPrdAccesorios { get; set; } = new List<DetPrdAccesorio>();

    public virtual ICollection<DetPrdPchMaquinaC> DetPrdPchMaquinaCs { get; set; } = new List<DetPrdPchMaquinaC>();
}
