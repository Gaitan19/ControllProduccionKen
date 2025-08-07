using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class PrdBloquesReporteDTO
    {
        // Encabezado
        public int Id { get; set; }
        public string Operarios { get; set; } = string.Empty;
        public string Supervisor { get; set; } = string.Empty;
        public string JefeProd { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }

        // Detalles
        public List<DetalleBloquesDto> Detalles { get; set; } = new List<DetalleBloquesDto>();
    }

    /// <summary>
    /// DTO de detalle para cada registro en DetPrdBloques.
    /// </summary>
    public class DetalleBloquesDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Id de la cabecera (PrdBloque).
        /// </summary>
        public int PrdBloqueId { get; set; }

        /// <summary>
        /// Artículo (Bloque + Medidas).
        /// </summary>
        public string Articulo { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de la máquina.
        /// </summary>
        public string Maquina { get; set; } = string.Empty;

        /// <summary>
        /// Producción del día.
        /// </summary>
        public decimal ProduccionDia { get; set; }

        // Subdetalles
        public List<SubDetalleBloquesDto> SubDetalles { get; set; } = new List<SubDetalleBloquesDto>();
    }

    /// <summary>
    /// DTO de subdetalle para cada registro en SubDetPrdBloques.
    /// </summary>
    public class SubDetalleBloquesDto
    {
        /// <summary>
        /// Id del registro de detalle al que pertenece.
        /// </summary>
        public int DetPrdBloquesId { get; set; }

        /// <summary>
        /// Número de elemento dentro del detalle.
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// Identificador del silo.
        /// </summary>
        public int Silo { get; set; }

        /// <summary>
        /// Código del bloque.
        /// </summary>
        public string CodigoBloque { get; set; } = string.Empty;

        /// <summary>
        /// Artículo (Bloque + Medidas).
        /// </summary>
        public string Articulo { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de fabricación.
        /// </summary>
        public string TipoFabricacion { get; set; } = string.Empty;

        /// <summary>
        /// Número de pedido, si aplica.
        /// </summary>
        public int? NumeroPedido { get; set; }

        /// <summary>
        /// Observaciones.
        /// </summary>
        public string? Observaciones { get; set; }

        /// <summary>
        /// Peso.
        /// </summary>
        public decimal Peso { get; set; }

        /// <summary>
        /// Hora de registro.
        /// </summary>
        public TimeSpan Hora { get; set; }

        /// <summary>
        /// Descripción del tipo de bloque.
        /// </summary>
        public string TipoBloque { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la densidad.
        /// </summary>
        public string Densidad { get; set; } = string.Empty;
    }
}
