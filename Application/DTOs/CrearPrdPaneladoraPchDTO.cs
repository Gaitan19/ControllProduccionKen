using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CrearPrdPaneladoraPchDTO
    {
        public List<MaquinaDto>? CatMaquina { get; set; }
        public List<TipoFabricacionDto>? CatTipoFabricacion { get; set; }
        public List<CatalogoPanelesPch>? CatalogoPanelesPch { get; set; }
    }
}