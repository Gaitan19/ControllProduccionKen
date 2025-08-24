using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class PrdAccesorioReporteDTO
    {
        // Encabezado
        public int Id { get; set; }
        
        // Usuarios (IDs crudos y nombres amigables)
        public string IdUsuarios { get; set; } = string.Empty;
        public string Operarios { get; set; } = string.Empty; // Friendly name

        // Máquina
        public int IdMaquina { get; set; }
        public string Maquina { get; set; } = string.Empty; // Friendly name

        // Datos generales
        public DateTime Fecha { get; set; }
        public string? Observaciones { get; set; }
        public decimal? TiempoParo { get; set; }

        // Mermas
        public decimal? MermaMallaCovintecKg { get; set; }
        public decimal? MermaMallaPchKg { get; set; }
        public decimal? MermaBobinasKg { get; set; }
        public decimal? MermaLitewallKg { get; set; }

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

        // Detalles
        public List<DetPrdAccesorioReporteDTO> Detalles { get; set; } = new List<DetPrdAccesorioReporteDTO>();
    }

    /// <summary>
    /// DTO de detalle para cada registro en DetPrdAccesorio.
    /// </summary>
    public class DetPrdAccesorioReporteDTO
    {
        /// <summary>
        /// Id de la cabecera (PrdAccesorio).
        /// </summary>
        public int IdAccesorio { get; set; }

        /// The "No." secuencial del detalle.
        public int No { get; set; }

        /// Nombre del tipo de artículo (friendly).
        public string TipoArticulo { get; set; } = string.Empty;

        /// Nombre del artículo (friendly).
        public string Articulo { get; set; } = string.Empty;

        /// Descripción del tipo de fabricación (friendly).
        public string TipoFabricacion { get; set; } = string.Empty;

        /// Número de pedido, si aplica.
        public int? NumeroPedido { get; set; }

        /// Malla Covintec (friendly).
        public string? MallaCovintec { get; set; }
        public int? CantidadMallaUn { get; set; }

        /// Tipo Malla PCH (friendly).
        public string? TipoMallaPch { get; set; }
        public decimal? CantidadPchKg { get; set; }

        /// Ancho Bobina (friendly).
        public string? AnchoBobina { get; set; }
        public decimal? CantidadBobinaKg { get; set; }

        /// Calibre (friendly).
        public string? Calibre { get; set; }
        public decimal? CantidadCalibreKg { get; set; }

      
    }
}