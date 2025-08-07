using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CatLaminaDTO
    {
        public int Id { get; set; }

        public int? IdLineaProduccion { get; set; }

        public string CodigoArticulo { get; set; } = null!;

        public string DescripcionArticulo { get; set; } = null!;

        public bool Activo { get; set; }
    }
}
