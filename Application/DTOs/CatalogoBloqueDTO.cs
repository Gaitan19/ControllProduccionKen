using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CatalogoBloqueDTO
    {
        public int Id { get; set; }

        public string? Bloque { get; set; }

        public decimal? CubicajeM3 { get; set; }

        public string? Medidas { get; set; }

        public bool? Activo { get; set; }
    }
}