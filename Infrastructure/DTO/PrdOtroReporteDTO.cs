using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class PrdOtroReporteDTO
    {
        // Encabezado
        public int Id { get; set; }
        public int? IdTipoReporte { get; set; }
        public string IdUsuarios { get; set; } = string.Empty;
        public string Operarios { get; set; } = string.Empty; // Friendly name
        public DateTime Fecha { get; set; }
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
        public List<DetalleOtroDto> Detalles { get; set; } = new List<DetalleOtroDto>();
    }

    /// <summary>
    /// DTO de detalle para cada registro en DetPrdOtro.
    /// </summary>
    public class DetalleOtroDto
    {
        /// <summary>
        /// Id de la cabecera (PrdOtro).
        /// </summary>
        public int IdOtro { get; set; }

        /// <summary>
        /// Actividad realizada.
        /// </summary>
        public string Actividad { get; set; } = string.Empty;

        /// <summary>
        /// Descripción del producto.
        /// </summary>
        public string DescripcionProducto { get; set; } = string.Empty;

        /// <summary>
        /// Descripción del tipo de fabricación.
        /// </summary>
        public string TipoFabricacion { get; set; } = string.Empty;

        /// <summary>
        /// Número de pedido, si aplica.
        /// </summary>
        public int? NumeroPedido { get; set; }

        /// <summary>
        /// Nota del proceso.
        /// </summary>
        public string Nota { get; set; } = string.Empty;

        /// <summary>
        /// Merma registrada.
        /// </summary>
        public string Merma { get; set; } = string.Empty;

        /// <summary>
        /// Comentario adicional.
        /// </summary>
        public string Comentario { get; set; } = string.Empty;

        /// <summary>
        /// Hora de inicio de la actividad.
        /// </summary>
        public TimeSpan HoraInicio { get; set; }

        /// <summary>
        /// Hora de fin de la actividad.
        /// </summary>
        public TimeSpan HoraFin { get; set; }
    }
}