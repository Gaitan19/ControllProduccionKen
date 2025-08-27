using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PreDetPrdpreExpansionDTO
    {
        public int Id { get; set; }

        public string MarcaTipo { get; set; } = null!;

        public string? CodigoSaco { get; set; }

        public string? Lote { get; set; }

        public DateTime? FechaProduccion { get; set; }

        public int PrdpreExpansionId { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public List<DetPrdpreExpansionDTO>? DetPrdpreExpansions { get; set; }
    }
}