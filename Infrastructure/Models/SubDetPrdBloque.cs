using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class SubDetPrdBloque
{
    public int Id { get; set; }

    public int? DetPrdBloquesId { get; set; }

    public int IdArticulo { get; set; }

    public int No { get; set; }

    public TimeSpan Hora { get; set; }

    public int Silo { get; set; }

    public int IdDensidad { get; set; }

    public int IdTipoBloque { get; set; }

    public decimal Peso { get; set; }

    public int IdTipoFabricacion { get; set; }

    public int? NumeroPedido { get; set; }

    public string CodigoBloque { get; set; } = null!;

    public string? Observaciones { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual DetPrdBloque? DetPrdBloques { get; set; }

    public virtual CatalogoBloque IdArticuloNavigation { get; set; } = null!;

    public virtual MaestroCatalogo IdDensidadNavigation { get; set; } = null!;

    public virtual MaestroCatalogo IdTipoBloqueNavigation { get; set; } = null!;

    public virtual TipoFabricacion IdTipoFabricacionNavigation { get; set; } = null!;
}
