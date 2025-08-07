using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.DTOs
{
    public class CrearPrdPanelesCovintecDto
    {
      
        public List<MaquinaDto>? CatMaquina { get; set; }

        public List<TipoFabricacionDto>? CatTipoFabricacion { get; set; }

        public List<CatalogoPanelesCovintecDTO>? CatalogoPaneles { get; set; }
    

    }
}
