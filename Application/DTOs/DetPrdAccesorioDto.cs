using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DetPrdAccesorioDto
    {
        public int Id { get; set; }
        public int PrdAccesoriosId { get; set; }
        public int IdTipoArticulo { get; set; }
        public int IdArticulo { get; set; }
        public int IdTipoFabricacion { get; set; }
        public int? NumeroPedido { get; set; }
        public int? IdMallaCovintec { get; set; }
        public int? CantidadMallaUn { get; set; }
        public int? IdTipoMallaPch { get; set; }
        public decimal? CantidadPchKg { get; set; }
        public int? IdAnchoBobina { get; set; }
        public decimal? CantidadBobinaKg { get; set; }
        public int? IdCalibre { get; set; }
        public decimal? CantidadCalibreKg { get; set; }
        
      
        public string IdUsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}