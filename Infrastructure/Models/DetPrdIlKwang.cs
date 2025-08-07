using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class DetPrdIlKwang
{
    public int Id { get; set; }

    public int PrdIlKwangId { get; set; }

    public int Posicion { get; set; }

    public int IdEspesor { get; set; }

    public int Cantidad { get; set; }

    public decimal Medida { get; set; }

    public decimal? MetrosCuadrados { get; set; }

    public int IdStatus { get; set; }

    public int IdTipo { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual CatEspesor IdEspesorNavigation { get; set; } = null!;

    public virtual CatalogoStatus IdStatusNavigation { get; set; } = null!;

    public virtual CatalogoTipo IdTipoNavigation { get; set; } = null!;
}
