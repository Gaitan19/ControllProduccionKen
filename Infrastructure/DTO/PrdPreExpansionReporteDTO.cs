using System;
using System.Collections.Generic;

namespace Infrastructure.DTO
{
    /// <summary>
    /// DTO de reporte para PrdpreExpansion (encabezado + detalles).
    /// </summary>
    public class PrdPreExpansionReporteDTO
    {
        // Encabezado
        public int Id { get; set; }
        public decimal? TiempoParo { get; set; }
        public int? IdTipoReporte { get; set; }
        public string IdUsuarios { get; set; } = string.Empty;
        public string Operarios { get; set; } = string.Empty; // Friendly name
        public int IdMaquina { get; set; }
        public string Maquina { get; set; } = string.Empty;   // Friendly name
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public decimal? PresionCaldera { get; set; }
        public string? Lote { get; set; }
        public DateTime? FechaProduccion { get; set; }
        public string? CodigoSaco { get; set; }
        public int? IdTipoFabricacion { get; set; }
        public string TipoFabricacion { get; set; } = string.Empty; // Friendly name
        public int? NumeroPedido { get; set; }
        public string? Observaciones { get; set; }
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

        // Detalles (PreDetPrdpreExpansion - nivel intermedio)
        public List<DetallePreExpansionDto> Detalles { get; set; } = new List<DetallePreExpansionDto>();
    }

    /// <summary>
    /// DTO de detalle para cada registro en PreDetPrdpreExpansion (nivel intermedio).
    /// </summary>
    public class DetallePreExpansionDto
    {
        /// <summary>Id del registro PreDetPrdpreExpansion.</summary>
        public int Id { get; set; }

        /// <summary>Id de la cabecera (PrdpreExpansion).</summary>
        public int PrdpreExpansionId { get; set; }

        /// <summary>Marca/Tipo del detalle.</summary>
        public string MarcaTipo { get; set; } = string.Empty;

        /// <summary>Código del saco.</summary>
        public string? CodigoSaco { get; set; }

        /// <summary>Lote del material.</summary>
        public string? Lote { get; set; }

        /// <summary>Fecha de producción.</summary>
        public DateTime? FechaProduccion { get; set; }

        // Sub-detalles (DetPrdpreExpansion - nivel sub-detalle)
        public List<SubDetallePreExpansionDto> SubDetalles { get; set; } = new List<SubDetallePreExpansionDto>();
    }

    /// <summary>
    /// DTO de sub-detalle para cada registro en DetPrdpreExpansion (nivel sub-detalle).
    /// </summary>
    public class SubDetallePreExpansionDto
    {
        /// <summary>Id del registro DetPrdpreExpansion.</summary>
        public int Id { get; set; }

        /// <summary>Id del detalle (PreDetPrdpreExpansion).</summary>
        public int PreDetPrdpreExpansionId { get; set; }

        /// <summary>Hora del batch (TimeSpan).</summary>
        public TimeSpan Hora { get; set; }

        /// <summary>Número de batch.</summary>
        public int NoBatch { get; set; }

        /// <summary>Densidad esperada.</summary>
        public decimal? DensidadEsperada { get; set; }

        /// <summary>Peso del batch en gramos.</summary>
        public decimal? PesoBatchGr { get; set; }

        /// <summary>Densidad real.</summary>
        public decimal? Densidad { get; set; }

        /// <summary>Kilogramos por batch.</summary>
        public decimal? KgPorBatch { get; set; }

        /// <summary>Presión medida en PSI.</summary>
        public decimal? PresionPSI { get; set; }

        /// <summary>Tiempo de batch en segundos.</summary>
        public decimal? TiempoBatchSeg { get; set; }

        /// <summary>Temperatura en °C.</summary>
        public decimal? TemperaturaC { get; set; }

        /// <summary>Identificador de silo.</summary>
        public int? Silo { get; set; }

        /// <summary>Paso (1=Primer Paso, 2=Segundo Paso).</summary>
        public int? Paso { get; set; }

        /// <summary>Auditoría: usuario creación.</summary>
        public string IdUsuarioCreacion { get; set; } = string.Empty;

        /// <summary>Auditoría: fecha creación.</summary>
        public DateTime FechaCreacion { get; set; }

        /// <summary>Auditoría: usuario actualización.</summary>
        public string? IdUsuarioActualizacion { get; set; }

        /// <summary>Auditoría: fecha actualización.</summary>
        public DateTime? FechaActualizacion { get; set; }
    }
}
