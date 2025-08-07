using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SubDetPrdBloqueDTO
    {
        public int Id { get; set; }

        public int? DetPrdBloquesId { get; set; }

        public int IdArticulo { get; set; }

        public int No { get; set; }

        public TimeSpan Hora { get; set; }

        public int Silo { get; set; }

        public int IdDensidad { get; set; }

        public int IdTipoBloque { get; set; }

        public decimal Peso { get; set; }

        public int IdTipoFabricacion { get; set; }

        public int? NumeroPedido { get; set; }

        public string CodigoBloque { get; set; } = null!;

        public string? Observaciones { get; set; }

       

        public string IdUsuarioCreacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}