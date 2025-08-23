using System;
using System.Collections.Generic;

namespace Infrastructure.DTO
{
    public class PrdMallaPCHReporteDTO
    {
        // Encabezado
        public int Id { get; set; }

        // Usuarios (IDs crudos y nombres amigables)
        public string IdUsuarios { get; set; } = string.Empty;
        public string Operarios { get; set; } = string.Empty; // Friendly name

        // Datos generales
        public DateTime Fecha { get; set; }
        public decimal? ProduccionDia { get; set; }
        public string? Observaciones { get; set; }

        // Auditoría y aprobaciones
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public bool AprobadoSupervisor { get; set; }
        public bool AprobadoGerencia { get; set; }
        public string? IdAprobadoSupervisor { get; set; }
        public string? IdAprobadoGerencia { get; set; }
        public string Supervisor { get; set; } = string.Empty; // Friendly name
        public string JefeProd { get; set; } = string.Empty;   // Friendly name

        // Detalles por máquina
        public List<DetallePchMaquinaADto> MaquinaA { get; set; } = new List<DetallePchMaquinaADto>();
        public List<DetallePchMaquinaBDto> MaquinaB { get; set; } = new List<DetallePchMaquinaBDto>();
        public List<DetallePchMaquinaCDto> MaquinaC { get; set; } = new List<DetallePchMaquinaCDto>();
    }

    /// <summary>
    /// Detalle para cp.DetPrdPchMaquinaA.
    /// </summary>
    public class DetallePchMaquinaADto
    {
        public int IdMallaPch { get; set; }          // D.PrdMallaPchId
        public int IdMaquina { get; set; }
        public string Maquina { get; set; } = string.Empty; // Friendly name

        public int HilosTransversalesUN { get; set; }
        public decimal? MermaHilosTransversalesKg { get; set; }

        public int IdTipoFabricacion { get; set; }
        public string TipoFabricacion { get; set; } = string.Empty; // Friendly name

        public int? NumeroPedido { get; set; }
        public decimal? Longitud { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Produccion { get; set; }

        public string? NumeroAlambreA { get; set; }
        public decimal? PesoAlambreKgA { get; set; }

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }

    /// <summary>
    /// Detalle para cp.DetPrdPchMaquinaB.
    /// </summary>
    public class DetallePchMaquinaBDto
    {
        public int IdMallaPch { get; set; }          // D.PrdMallaPchId
        public int IdMaquina { get; set; }
        public string Maquina { get; set; } = string.Empty; // Friendly name

        public int HilosLongitudinalesUN { get; set; }
        public decimal? MermaHilosLongitudinalesKg { get; set; }

        public int IdTipoFabricacion { get; set; }
        public string TipoFabricacion { get; set; } = string.Empty; // Friendly name

        public int? NumeroPedido { get; set; }
        public decimal? Longitud { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Produccion { get; set; }

        public string? NumeroAlambreB { get; set; }
        public decimal? PesoAlambreKgB { get; set; }

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }

    /// <summary>
    /// Detalle para cp.DetPrdPchMaquinaC.
    /// </summary>
    public class DetallePchMaquinaCDto
    {
        public int IdMallaPch { get; set; }          // D.PrdMallaPchId
        public int IdMaquina { get; set; }
        public string Maquina { get; set; } = string.Empty; // Friendly name

        public int IdTipoMalla { get; set; }
        public string TipoMalla { get; set; } = string.Empty;   // cp.CatTipoMalla.Cuadricula
        public string? PesoMallaM2 { get; set; }                // cp.CatTipoMalla.PesoPorMts2 (puede ser texto)

        public decimal? MermaMallasKg { get; set; }

        public int IdTipoFabricacion { get; set; }
        public string TipoFabricacion { get; set; } = string.Empty; // Friendly name

        public int? NumeroPedido { get; set; }
        public decimal? Longitud { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Produccion { get; set; }

        // Auditoría
        public string IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
