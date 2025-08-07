using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CatEspesorDTO
    {
        public int Id { get; set; }

        public string Valor { get; set; } = null!;

        public bool Activo { get; set; }
    }
}
