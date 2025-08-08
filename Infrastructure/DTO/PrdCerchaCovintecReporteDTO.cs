using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class PrdCerchaCovintecReporteDTO
    {
        // Encabezado
        public int Id { get; set; }
        public string Operarios { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int ConteoInicial { get; set; }
        public int ConteoFinal { get; set; }
        public string Maquina { get; set; } = string.Empty;
        public string Observaciones { get; set; } = string.Empty;
        public decimal MermaAlambre { get; set; }
        public string Supervisor { get; set; } = string.Empty;
        public string JefeProd { get; set; } = string.Empty;
        public decimal TiempoParo { get; set; }

        // Detalles
        public List<DetalleCerchaDto> DetallesPanel { get; set; } = new List<DetalleCerchaDto>();
        public List<DetalleAlambreCerchaDto> DetallesAlambre { get; set; } = new List<DetalleAlambreCerchaDto>();
    }

    public class DetalleCerchaDto
    {
        // Usamos esta columna para relacionarlo con el encabezado.
        public int IdCercha { get; set; }
        public string Articulo { get; set; } = string.Empty;
        public int CantidadProducida { get; set; }
        public int CantidadNoConforme { get; set; }
        public string TipoFabricacion { get; set; } = string.Empty;
        public string NumeroPedido { get; set; } = string.Empty;
        public decimal ProduccionDia { get; set; }
    }

    public class DetalleAlambreCerchaDto
    {
        public int IdCercha { get; set; }
        public string NumeroAlambre { get; set; } = string.Empty;
        public decimal PesoAlambre { get; set; }
    }
}
