using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class DetPrdOtro
{
    public int Id { get; set; }

    public int PrdOtroId { get; set; }

    public string Actividad { get; set; } = null!;

    public string DescripcionProducto { get; set; } = null!;

    public int IdTipoFabricacion { get; set; }

    public int? NumeroPedido { get; set; }

    public string Nota { get; set; } = null!;

    public string Merma { get; set; } = null!;

    public string Comentario { get; set; } = null!;

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFin { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public decimal Cantidad { get; set; }

    public string UnidadMedida { get; set; } = null!;

    public virtual TipoFabricacion IdTipoFabricacionNavigation { get; set; } = null!;

    public virtual PrdOtro PrdOtro { get; set; } = null!;
}
