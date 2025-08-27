using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DetPrdpreExpansionDTO
    {
        public int Id { get; set; }
        public int PreDetPrdpreExpansionId { get; set; }

        public TimeSpan Hora { get; set; }
        public int NoBatch { get; set; }

        public int DensidadEsperada { get; set; }
        public decimal PesoBatchGr { get; set; }
        public decimal Densidad { get; set; }
        public int KgPorBatch { get; set; }
        public int PresionPsi { get; set; }
        public int TiempoBatchSeg { get; set; }
        public int TemperaturaC { get; set; }
        public int Silo { get; set; }

        public int? Paso { get; set; }
        public string IdUsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
