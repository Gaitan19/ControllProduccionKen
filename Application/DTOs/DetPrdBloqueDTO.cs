using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DetPrdBloqueDTO
    {
        public int Id { get; set; }

        public int PrdBloqueId { get; set; }

        public int IdMaquina { get; set; }

        public int IdCatBloque { get; set; }
        public decimal? ProduccionDia { get; set; }

        public string IdUsuarioCreacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public List<SubDetPrdBloqueDTO>? SubDetPrdBloques { get; set; }
    }
}