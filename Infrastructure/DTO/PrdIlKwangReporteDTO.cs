using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class PrdIlKwangReporteDTO
    {
        // --- Encabezado / Datos Generales ---
        public int Id { get; set; }
        public string Operarios { get; set; }
        public string Articulos { get; set; }
        public DateTime Fecha { get; set; }
        public string Maquina { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Cliente { get; set; }
        public int? NumeroPedido { get; set; }
        public string TipoFabricacion { get; set; }
        public decimal? TiempoParo { get; set; }
        public string Observaciones { get; set; }
        public string Supervisor { get; set; }
        public string JefeProd { get; set; }

        // --- Información de Bobinas (original) ---
        public string FabricanteBobinaA { get; set; }
        public string CodigoBobinaA { get; set; }
        public string UbicacionBobinaA { get; set; }
        public string ColorBobinaA { get; set; }
        public string FabricanteBobinaB { get; set; }
        public string CodigoBobinaB { get; set; }
        public string UbicacionBobinaB { get; set; }
        public string ColorBobinaB { get; set; }

        // --- Datos adicionales de Bobinas A ---
        public decimal CalibreMmA { get; set; }
        public int AnchoMmA { get; set; }
        public int PesoInicialKgA { get; set; }
        public int PesoFinalKgA { get; set; }
        public int MetrosInicialA { get; set; }
        public int MetrosFinalA { get; set; }
        public int EspesorInicialCmA { get; set; }
        public int EspesorFinalCmA { get; set; }
        public decimal? ConsumoBobinaKgA { get; set; }
        public decimal PesoInicialA { get; set; }
        public decimal CantidadUtilizadaA { get; set; }
        public decimal PesoFinalA { get; set; }
        public decimal VelocidadSuperiorA { get; set; }
        public decimal VelocidadInferiorA { get; set; }
        public string LoteA { get; set; }
        public DateTime VencimientoA { get; set; }

        // --- Datos adicionales de Bobinas B ---
        public decimal CalibreMmB { get; set; }
        public int AnchoMmB { get; set; }
        public int PesoInicialKgB { get; set; }
        public int PesoFinalKgB { get; set; }
        public int MetrosInicialB { get; set; }
        public int MetrosFinalB { get; set; }
        public int EspesorInicialCmB { get; set; }
        public int EspesorFinalCmB { get; set; }
        public decimal? ConsumoBobinaKgB { get; set; }
        public decimal PesoInicialB { get; set; }
        public decimal CantidadUtilizadaB { get; set; }
        public decimal PesoFinalB { get; set; }
        public decimal VelocidadSuperiorB { get; set; }
        public decimal VelocidadInferiorB { get; set; }
        public string LoteB { get; set; }
        public DateTime VencimientoB { get; set; }

        // --- Producción / Totales ---
        public int VelocidadMaquina { get; set; }
        public decimal? TotalProduccion { get; set; }
        public decimal? MetrosConDefecto { get; set; }
        public decimal? MermaM { get; set; }
        public int? CantidadArranques { get; set; }
        public decimal? MetrosAdicionales { get; set; }
        public decimal? PorcentajeMerma { get; set; }
        public decimal? PorcentajeDefecto { get; set; }

        // --- Estados y Trazabilidad ---
        //public bool AprobadoSupervisor { get; set; }
        //public bool AprobadoGerencia { get; set; }
        //public DateTime FechaCreacion { get; set; }
        //public DateTime? FechaActualizacion { get; set; }
        // Detalles
        public List<DetalleIlKwangDto> DetallesProduccion { get; set; } = new List<DetalleIlKwangDto>();
    }

    public class DetalleIlKwangDto
    {
        // Usamos esta columna para relacionarlo con el encabezado.
        public int IdPrdIlKwang { get; set; }
        public int Posicion { get; set; }
        public string Espesor { get; set; }
        public int Cantidad { get; set; }
        public decimal Medida { get; set; }
        public decimal? MetrosCuadrados { get; set; }
        public string Status { get; set; }
        public string Tipo { get; set; }
    }
}