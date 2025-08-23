using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class DetPrdpreExpansion
{
    public int Id { get; set; }

    public int PrdpreExpansionId { get; set; }

    public TimeSpan Hora { get; set; }

    public int NoBatch { get; set; }

    public int DensidadEsperada { get; set; }

    public decimal PesoBatchGr { get; set; }

    public decimal Densidad { get; set; }

    public int KgPorBatch { get; set; }

    public int PresionPsi { get; set; }

    public int TiempoBatchSeg { get; set; }

    public int TemperaturaC { get; set; }

    public int Silo { get; set; }

    public int? Paso { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual PrdpreExpansion PrdpreExpansion { get; set; } = null!;
}
