using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class PrdNeveraReporteDTO
    {
        // Encabezado
        public int Id { get; set; }
        public decimal? TiempoParo { get; set; }
        public int? IdTipoReporte { get; set; }
        public string IdUsuarios { get; set; } = string.Empty;
        public string Operarios { get; set; } = string.Empty; // Friendly name
        public int IdMaquina { get; set; }
        public string Maquina { get; set; } = string.Empty; // Friendly name
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
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
        public string JefeProd { get; set; } = string.Empty; // Friendly name

        // Detalles
        public List<DetalleNeveraDto> Detalles { get; set; } = new List<DetalleNeveraDto>();
    }

    /// <summary>
    /// DTO de detalle para cada registro en DetPrdNevera.
    /// </summary>
    public class DetalleNeveraDto
    {
        /// <summary>
        /// Id de la cabecera (PrdNevera).
        /// </summary>
        public int IdNevera { get; set; }

        /// <summary>
        /// Posición dentro de la nevera.
        /// </summary>
        public int Posicion { get; set; }

        /// <summary>
        /// Nombre del artículo.
        /// </summary>
        public string Articulo { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad conforme producida.
        /// </summary>
        public decimal CantidadConforme { get; set; }

        /// <summary>
        /// Cantidad no conforme.
        /// </summary>
        public decimal CantidadNoConforme { get; set; }

        /// <summary>
        /// Descripción del tipo de fabricación.
        /// </summary>
        public string TipoFabricacion { get; set; } = string.Empty;

        /// <summary>
        /// Número de pedido, si aplica.
        /// </summary>
        public int? NumeroPedido { get; set; }
    }
}
