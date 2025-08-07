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
        public string Operarios { get; set; }
        public DateTime Fecha { get; set; }
        public int ConteoInicial { get; set; }
        public int ConteoFinal { get; set; }
        public string Maquina { get; set; }
        public string Observaciones { get; set; }
        public decimal MermaAlambre { get; set; }
        public string Supervisor { get; set; }
        public string JefeProd { get; set; }
        public decimal TiempoParo { get; set; }

        // Detalles
        public List<DetalleCerchaDto> DetallesPanel { get; set; } = new List<DetalleCerchaDto>();
        public List<DetalleAlambreCerchaDto> DetallesAlambre { get; set; } = new List<DetalleAlambreCerchaDto>();
    }

    public class DetalleCerchaDto
    {
        // Usamos esta columna para relacionarlo con el encabezado.
        public int IdCercha { get; set; }
        public string Articulo { get; set; }
        public int CantidadProducida { get; set; }
        public int CantidadNoConforme { get; set; }
        public string TipoFabricacion { get; set; }
        public string NumeroPedido { get; set; }
        public decimal ProduccionDia { get; set; }
    }

    public class DetalleAlambreCerchaDto
    {
        public int IdCercha { get; set; }
        public string NumeroAlambre { get; set; }
        public decimal PesoAlambre { get; set; }
    }
}
