using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class DetPrdNevera
{
    public int Id { get; set; }

    public int PrdNeveraId { get; set; }

    public int Posicion { get; set; }

    public int IdArticulo { get; set; }

    public decimal CantidadConforme { get; set; }

    public decimal CantidadNoConforme { get; set; }

    public int IdTipoFabricacion { get; set; }

    public int? NumeroPedido { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual MaestroCatalogo IdArticuloNavigation { get; set; } = null!;

    public virtual TipoFabricacion IdTipoFabricacionNavigation { get; set; } = null!;

    public virtual PrdNevera PrdNevera { get; set; } = null!;
}
