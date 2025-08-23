using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class DetPrdAccesorio
{
    public int Id { get; set; }

    public int PrdAccesoriosId { get; set; }

    public int IdTipoArticulo { get; set; }

    public int IdArticulo { get; set; }

    public int IdTipoFabricacion { get; set; }

    public int? NumeroPedido { get; set; }

    public int? IdMallaCovintec { get; set; }

    public int? CantidadMallaUn { get; set; }

    public int? IdTipoMallaPch { get; set; }

    public decimal? CantidadPchKg { get; set; }

    public int? IdAnchoBobina { get; set; }

    public decimal? CantidadBobinaKg { get; set; }

    public int? IdCalibre { get; set; }

    public decimal? CantidadCalibreKg { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual AnchoBobina? IdAnchoBobinaNavigation { get; set; }

    public virtual CatalogoAccesorio IdArticuloNavigation { get; set; } = null!;

    public virtual MaestroCatalogo? IdCalibreNavigation { get; set; }

    public virtual CatalogoMallasCovintec? IdMallaCovintecNavigation { get; set; }

    public virtual MaestroCatalogo IdTipoArticuloNavigation { get; set; } = null!;

    public virtual TipoFabricacion IdTipoFabricacionNavigation { get; set; } = null!;

    public virtual CatTipoMalla? IdTipoMallaPchNavigation { get; set; }
}
