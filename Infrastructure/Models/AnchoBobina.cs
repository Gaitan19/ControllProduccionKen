using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class AnchoBobina
{
    public int Id { get; set; }

    public int Valor { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<PrdIlKwang> PrdIlKwangIdAnchoBobinaANavigations { get; set; } = new List<PrdIlKwang>();

    public virtual ICollection<PrdIlKwang> PrdIlKwangIdAnchoBobinaBNavigations { get; set; } = new List<PrdIlKwang>();
}
