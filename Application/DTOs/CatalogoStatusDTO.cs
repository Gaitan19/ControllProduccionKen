using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CatalogoStatusDTO
    {
        public int Id { get; set; }

        public string Descripcion { get; set; } = null!;

        public bool? Activo { get; set; }
    }
}
