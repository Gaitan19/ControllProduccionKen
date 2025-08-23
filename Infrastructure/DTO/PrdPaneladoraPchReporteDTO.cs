using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class PrdPaneladoraPchReporteDTO
    {
        // Encabezado
        public int Id { get; set; }
        public decimal? TiempoParo { get; set; }
        public decimal? ProduccionDia { get; set; }

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
        public List<DetallePaneladoraPchDto> Detalles { get; set; } = new List<DetallePaneladoraPchDto>();
    }

    /// <summary>
    /// DTO de detalle para cada registro en DetPrdPaneladoraPch.
    /// </summary>
    public class DetallePaneladoraPchDto
    {
        /// <summary>
        /// Id de la cabecera (PrdPaneladoraPch).
        /// </summary>
        public int IdPaneladoraPch { get; set; }

        /// Nombre del artículo (friendly).
        public string Articulo { get; set; } = string.Empty;

        /// Longitud del panel.
        public decimal Longitud { get; set; }

        /// Cantidad producida conforme.
        public int CantidadProducida { get; set; }

        /// Cantidad no conforme.
        public int CantidadNoConforme { get; set; }

        /// Metros cuadrados por panel.
        public decimal Mts2PorPanel { get; set; }

        /// Descripción del tipo de fabricación (friendly).
        public string TipoFabricacion { get; set; } = string.Empty;

        /// Número de pedido, si aplica.
        public int? NumeroPedido { get; set; }

        /// Número de alambre utilizado.
        public int NumeroAlambre { get; set; }

        /// Peso del alambre en kg.
        public decimal PesoAlambreKg { get; set; }

        /// Merma del alambre en kg.
        public decimal MermaAlambreKg { get; set; }
    }
}