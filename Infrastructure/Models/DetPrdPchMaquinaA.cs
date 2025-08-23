using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class DetPrdPchMaquinaA
{
    public int Id { get; set; }

    public int PrdMallaPchId { get; set; }

    public int IdMaquina { get; set; }

    public int HilosTransversalesUn { get; set; }

    public decimal MermaHilosTransversalesKg { get; set; }

    public int IdTipoFabricacion { get; set; }

    public int? NumeroPedido { get; set; }

    public decimal Longitud { get; set; }

    public int Cantidad { get; set; }

    public decimal Produccion { get; set; }

    public int NumeroAlambreA { get; set; }

    public decimal PesoAlambreKgA { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual Maquina IdMaquinaNavigation { get; set; } = null!;

    public virtual TipoFabricacion IdTipoFabricacionNavigation { get; set; } = null!;

    public virtual PrdMallaPch PrdMallaPch { get; set; } = null!;
}
