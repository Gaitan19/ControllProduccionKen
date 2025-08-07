using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class MaestroCatalogo
{
    public int Id { get; set; }

    public int IdPadre { get; set; }

    public string? Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<DetPrdNevera> DetPrdNeveras { get; set; } = new List<DetPrdNevera>();

    public virtual ICollection<SubDetPrdBloque> SubDetPrdBloqueIdDensidadNavigations { get; set; } = new List<SubDetPrdBloque>();

    public virtual ICollection<SubDetPrdBloque> SubDetPrdBloqueIdTipoBloqueNavigations { get; set; } = new List<SubDetPrdBloque>();
}
