using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class ColoresBobina
{
    public int Id { get; set; }

    public string Color { get; set; } = null!;

    public string? Ral { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<PrdIlKwang> PrdIlKwangIdColorBobinaANavigations { get; set; } = new List<PrdIlKwang>();

    public virtual ICollection<PrdIlKwang> PrdIlKwangIdColorBobinaBNavigations { get; set; } = new List<PrdIlKwang>();
}
