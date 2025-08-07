using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public  class PrdCorteTReporteDTO
    {
        // Encabezado
        public int Id { get; set; }
        public decimal? TiempoParo { get; set; }
        public int? IdTipoReporte { get; set; }

        // Usuarios (IDs crudos y nombres amigables)
        public string IdUsuarios { get; set; } = string.Empty;
        public string Operarios { get; set; } = string.Empty; // Friendly name

        // Máquina
        public int IdMaquina { get; set; }
        public string Maquina { get; set; } = string.Empty; // Friendly name

        // Datos generales
        public DateTime Fecha { get; set; }
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

        // Detalles
        public List<DetalleCorteTDto> Detalles { get; set; } = new List<DetalleCorteTDto>();
    }

    /// <summary>
    /// DTO de detalle para cada registro en DetPrdCorteT.
    /// </summary>
    public class DetalleCorteTDto
    {
        /// <summary>
        /// Id de la cabecera (PrdCorteT).
        /// </summary>
        public int IdCorteT { get; set; }

        /// The "No." secuencial del detalle.
        public int No { get; set; }

        /// Nombre del artículo (friendly).
        public string Articulo { get; set; } = string.Empty;

        /// Descripción del tipo de fabricación (friendly).
        public string TipoFabricacion { get; set; } = string.Empty;

        /// Número de pedido, si aplica.
        public int? NumeroPedido { get; set; }

        /// Código de bloque.
        public string PrdCodigoBloque { get; set; } = string.Empty;

        /// Descripción de la densidad (friendly).
        public string Densidad { get; set; } = string.Empty;

        /// Descripción del tipo de bloque (friendly).
        public string TipoBloque { get; set; } = string.Empty;

        /// Cantidad conforme producida.
        public decimal CantidadConforme { get; set; }

        /// Cantidad no conforme producida.
        public decimal CantidadNoConforme { get; set; }

        /// Nota adicional del detalle.
        public string? Nota { get; set; }
    }
}
