using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CatalogoPanelesCovintecDTO
    {
        public int Id { get; set; }

        public int IdLineaProduccion { get; set; }

        public string DescripcionArticulo { get; set; } = null!;

        public string CodigoArticulo { get; set; } = null!;

        public decimal Mts2PorPanel { get; set; }

        public bool? Activo { get; set; }
    }
}
