using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class UbicacionBobina
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<PrdIlKwang> PrdIlKwangs { get; set; } = new List<PrdIlKwang>();
}
