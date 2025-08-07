using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class CatEspesor
{
    public int Id { get; set; }

    public string Valor { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<DetPrdIlKwang> DetPrdIlKwangs { get; set; } = new List<DetPrdIlKwang>();
}
