using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DetPrdCerchaCovintecDTO
    {
        public int Id { get; set; }
        public int? detrpdId { get; set; }

        public int IdCercha { get; set; }

        public int IdArticulo { get; set; }

        public int CantidadProducida { get; set; }

        public int CantidadNoConforme { get; set; }

        public int IdTipoFabricacion { get; set; }

        public int? NumeroPedido { get; set; }

        public decimal ProduccionDia { get; set; }

        public bool AprobadoSupervisor { get; set; }

        public bool AprobadoGerencia { get; set; }

        public string? IdAprobadoSupervisor { get; set; }

        public string? IdAprobadoGerencia { get; set; }

        public string IdUsuarioCreacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
