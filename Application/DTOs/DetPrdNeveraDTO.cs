using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public  class DetPrdNeveraDTO
    {
        public int Id { get; set; }
        public int PrdNeveraId { get; set; }
        public int Posicion { get; set; }

        public int IdArticulo { get; set; }
        public List<MaestroCatalogo>? CatMaestroCatalogo { get; set; }

        public decimal CantidadConforme { get; set; }
        public decimal CantidadNoConforme { get; set; }

        public int IdTipoFabricacion { get; set; }
        public List<TipoFabricacionDto>? CatTipoFabricacion { get; set; }

        public int? NumeroPedido { get; set; }

        public string IdUsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
