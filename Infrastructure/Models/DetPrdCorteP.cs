using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class DetPrdCorteP
{
    public int Id { get; set; }

    public int PrdCortePid { get; set; }

    public int No { get; set; }

    public int IdArticulo { get; set; }

    public int? NumeroPedido { get; set; }

    public int IdTipoFabricacion { get; set; }

    public string PrdCodigoBloque { get; set; } = null!;

    public int IdDensidad { get; set; }

    public int IdTipoBloque { get; set; }

    public decimal CantidadPiezasConformes { get; set; }

    public decimal CantidadPiezasNoConformes { get; set; }

    public string? Nota { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual CatPantografo IdArticuloNavigation { get; set; } = null!;

    public virtual CatalogoTipo IdTipoFabricacionNavigation { get; set; } = null!;

    public virtual PrdCorteP PrdCorteP { get; set; } = null!;
}
