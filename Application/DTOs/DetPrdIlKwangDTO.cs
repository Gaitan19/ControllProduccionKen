using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DetPrdIlKwangDTO
    {
        public int Id { get; set; }

        public int PrdIlKwangId { get; set; }

        public int Posicion { get; set; }

        public int IdEspesor { get; set; }

        public int Cantidad { get; set; }

        public decimal Medida { get; set; }

        public decimal? MetrosCuadrados { get; set; }

        public int IdStatus { get; set; }

        public int IdTipo { get; set; }

        public string IdUsuarioCreacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

      
    }
}
