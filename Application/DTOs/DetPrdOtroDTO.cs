using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DetPrdOtroDTO
    {
        public int Id { get; set; }
        public int PrdOtroId { get; set; }
        public string Actividad { get; set; } = null!;
        public string DescripcionProducto { get; set; } = null!;
        public int IdTipoFabricacion { get; set; }
        public int? NumeroPedido { get; set; }
        public string Nota { get; set; } = null!;
        public string Merma { get; set; } = null!;
        public string Comentario { get; set; } = null!;
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; } = null!;
        
        public string IdUsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}