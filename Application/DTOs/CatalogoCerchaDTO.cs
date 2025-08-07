using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CatalogoCerchaDTO
    {
        public int Id { get; set; }

        public int IdLineaProduccion { get; set; }

        public string CodigoArticulo { get; set; } = null!;

        public string DescripcionArticulo { get; set; } = null!;

        public decimal LongitudMetros { get; set; }

        public decimal? EspesorMetros { get; set; }

        public decimal? AreaM2 { get; set; }

        public bool? Activo { get; set; }

    }
}
