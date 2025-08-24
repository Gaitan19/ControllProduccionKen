using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class PrdMallaPchDTO
    {
        public int Id { get; set; }

        // Lista de IDs de usuarios (en entidad es string, aquí sigue el estilo de lista)
        public List<string> IdUsuarios { get; set; } = null!;

        public DateTime Fecha { get; set; }

        public string? Observaciones { get; set; }

        public decimal? ProduccionDia { get; set; }

        public string IdUsuarioCreacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public bool AprobadoSupervisor { get; set; }

        public bool AprobadoGerencia { get; set; }

        public string? IdAprobadoSupervisor { get; set; }

        public string? IdAprobadoGerencia { get; set; }

        public decimal? TiempoParo { get; set; }

        public List<DetPrdPchMaquinaADTO>? DetPrdPchMaquinaAs { get; set; }

        public List<DetPrdPchMaquinaBDTO>? DetPrdPchMaquinaBs { get; set; }

        public List<DetPrdPchMaquinaCDTO>? DetPrdPchMaquinaCs { get; set; }
    }

    public class DetPrdPchMaquinaADTO
    {
        public int Id { get; set; }

        public int PrdMallaPchId { get; set; }

        public int IdMaquina { get; set; }

        public int HilosTransversalesUn { get; set; }

        public decimal MermaHilosTransversalesKg { get; set; }

        public int IdTipoFabricacion { get; set; }

        public int? NumeroPedido { get; set; }

        public decimal Longitud { get; set; }

        public int Cantidad { get; set; }

        public decimal Produccion { get; set; }

        public int NumeroAlambreA { get; set; }

        public decimal PesoAlambreKgA { get; set; }

        public string IdUsuarioCreacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }

    public class DetPrdPchMaquinaBDTO
    {
        public int Id { get; set; }

        public int PrdMallaPchId { get; set; }

        public int IdMaquina { get; set; }

        public int HilosLongitudinalesUn { get; set; }

        public decimal MermaHilosLongitudinalesKg { get; set; }

        public int IdTipoFabricacion { get; set; }

        public int? NumeroPedido { get; set; }

        public decimal Longitud { get; set; }

        public int Cantidad { get; set; }

        public decimal Produccion { get; set; }

        public int NumeroAlambreB { get; set; }

        public decimal PesoAlambreKgB { get; set; }

        public string IdUsuarioCreacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }

    public class DetPrdPchMaquinaCDTO
    {
        public int Id { get; set; }

        public int PrdMallaPchId { get; set; }

        public int IdMaquina { get; set; }

        public int IdTipoMalla { get; set; }

        public decimal MermaMallasKg { get; set; }

        public int IdTipoFabricacion { get; set; }

        public int? NumeroPedido { get; set; }

        public decimal Longitud { get; set; }

        public int Cantidad { get; set; }

        public decimal Produccion { get; set; }

        public string IdUsuarioCreacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public string? IdUsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
