using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    //esta clase e generica para la mayoria de los reportes de paneles
    public class ShowPrdPanelesCovintecDto
    {
        public int Id { get; set; }
        public int? IdTipoReporte { get; set; }
        public string Operarios { get; set; }

        public string Maquina { get; set; }
      
        public DateTime? Fecha { get; set; }
     
        public bool AprobadoSupervisor { get; set; }
        public bool AprobadoGerencia { get; set; }
      
       
        public string? IdAprobadoSupervisor { get; set; }
        public string? IdAprobadoGerencia { get; set; }
    }
}
