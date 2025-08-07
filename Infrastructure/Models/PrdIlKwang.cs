using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class PrdIlKwang
{
    public int Id { get; set; }

    public int? IdTipoReporte { get; set; }

    public string IdUsuarios { get; set; } = null!;

    public int IdMaquina { get; set; }

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFin { get; set; }

    public DateTime Fecha { get; set; }

    public decimal? TiempoParo { get; set; }

    public string IdArticulo { get; set; } = null!;

    public int IdTipoFabricacion { get; set; }

    public string Cliente { get; set; } = null!;

    public int? NumeroPedido { get; set; }

    public int VelocidadMaquina { get; set; }

    public int IdUbicacionBobinaA { get; set; }

    public int IdAnchoBobinaA { get; set; }

    public string FabricanteBobinaA { get; set; } = null!;

    public string CodigoBobinaA { get; set; } = null!;

    public decimal CalibreMmA { get; set; }

    public int IdColorBobinaA { get; set; }

    public int AnchoMmA { get; set; }

    public int PesoInicialKgA { get; set; }

    public int PesoFinalKgA { get; set; }

    public int MetrosInicialA { get; set; }

    public int MetrosFinalA { get; set; }

    public int EspesorInicialCmA { get; set; }

    public int EspesorFinalCmA { get; set; }

    public decimal? ConsumoBobinaKgA { get; set; }

    public int IdUbicacionBobinaB { get; set; }

    public int IdAnchoBobinaB { get; set; }

    public string FabricanteBobinaB { get; set; } = null!;

    public string CodigoBobinaB { get; set; } = null!;

    public decimal CalibreMmB { get; set; }

    public int IdColorBobinaB { get; set; }

    public int AnchoMmB { get; set; }

    public int PesoInicialKgB { get; set; }

    public int PesoFinalKgB { get; set; }

    public int MetrosInicialB { get; set; }

    public int MetrosFinalB { get; set; }

    public int EspesorInicialCmB { get; set; }

    public int EspesorFinalCmB { get; set; }

    public decimal? ConsumoBobinaKgB { get; set; }

    public decimal PesoInicialA { get; set; }

    public decimal CantidadUtilizadaA { get; set; }

    public decimal PesoFinalA { get; set; }

    public decimal VelocidadSuperiorA { get; set; }

    public decimal VelocidadInferiorA { get; set; }

    public string LoteA { get; set; } = null!;

    public DateTime VencimientoA { get; set; }

    public decimal PesoInicialB { get; set; }

    public decimal CantidadUtilizadaB { get; set; }

    public decimal PesoFinalB { get; set; }

    public decimal VelocidadSuperiorB { get; set; }

    public decimal VelocidadInferiorB { get; set; }

    public string LoteB { get; set; } = null!;

    public DateTime VencimientoB { get; set; }

    public decimal? TotalProduccion { get; set; }

    public decimal? MetrosConDefecto { get; set; }

    public decimal? MermaM { get; set; }

    public decimal? MetrosAdicionales { get; set; }

    public decimal? PorcentajeMerma { get; set; }

    public decimal? PorcentajeDefecto { get; set; }

    public string? Observaciones { get; set; }

    public int? CantidadArranques { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public bool AprobadoSupervisor { get; set; }

    public bool AprobadoGerencia { get; set; }

    public string? IdAprobadoSupervisor { get; set; }

    public string? IdAprobadoGerencia { get; set; }

    public virtual AnchoBobina IdAnchoBobinaANavigation { get; set; } = null!;

    public virtual AnchoBobina IdAnchoBobinaBNavigation { get; set; } = null!;

    public virtual ColoresBobina IdColorBobinaANavigation { get; set; } = null!;

    public virtual ColoresBobina IdColorBobinaBNavigation { get; set; } = null!;

    public virtual Maquina IdMaquinaNavigation { get; set; } = null!;

    public virtual TipoFabricacion IdTipoFabricacionNavigation { get; set; } = null!;

    public virtual UbicacionBobina IdUbicacionBobinaANavigation { get; set; } = null!;
}
