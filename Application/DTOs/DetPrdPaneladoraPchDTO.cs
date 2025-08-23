using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DetPrdPaneladoraPchDTO
    {
        public int Id { get; set; }
        public int PrdPaneladoraPchId { get; set; }
        public int IdArticulo { get; set; }
        public decimal Longitud { get; set; }
        public int CantidadProducida { get; set; }
        public int CantidadNoConforme { get; set; }
        public decimal Mts2PorPanel { get; set; }
        public int IdTipoFabricacion { get; set; }
        public int? NumeroPedido { get; set; }
        public int NumeroAlambre { get; set; }
        public decimal PesoAlambreKg { get; set; }
        public decimal MermaAlambreKg { get; set; }
        public string IdUsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}