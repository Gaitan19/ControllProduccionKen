using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class CatalogoTipo
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<DetPrdCorteP> DetPrdCortePs { get; set; } = new List<DetPrdCorteP>();

    public virtual ICollection<DetPrdCorteT> DetPrdCorteTs { get; set; } = new List<DetPrdCorteT>();

    public virtual ICollection<DetPrdIlKwang> DetPrdIlKwangs { get; set; } = new List<DetPrdIlKwang>();
}
