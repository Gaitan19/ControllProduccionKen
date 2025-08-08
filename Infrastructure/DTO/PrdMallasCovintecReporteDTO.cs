using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class PrdMallasCovintecReporteDTO
    {
        // Encabezado
        public int Id { get; set; }
        public string Operarios { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Maquina { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public decimal MermaAlambre { get; set; }
        public string Supervisor { get; set; } = string.Empty;
        public string JefeProd { get; set; } = string.Empty;
        public decimal TiempoParo { get; set; }

        // Detalles
        public List<DetalleMallaDto> DetallesPanel { get; set; } = new List<DetalleMallaDto>();
        public List<DetalleAlambreMallaDto> DetallesAlambre { get; set; } = new List<DetalleAlambreMallaDto>();
    }
    public class DetalleMallaDto
    {
        // Usamos esta columna para relacionarlo con el encabezado.
        public int IdPanel { get; set; }
        public string Articulo { get; set; } = string.Empty;
        public int CantidadProducida { get; set; }
        public int CantidadNoConforme { get; set; }
        public string TipoFabricacion { get; set; } = string.Empty;
        public string NumeroPedido { get; set; } = string.Empty;
        public decimal ProduccionDia { get; set; }
    }

    public class DetalleAlambreMallaDto
    {
        public int IdPanel { get; set; }
        public string NumeroAlambre { get; set; } = string.Empty;
        public decimal PesoAlambre { get; set; }
    }
}
