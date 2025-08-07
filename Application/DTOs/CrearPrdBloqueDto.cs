using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CrearPrdBloqueDto
    {
        public List<MaquinaDto>? CatMaquina { get; set; }

        public List<TipoFabricacionDto>? CatTipoFabricacion { get; set; }

        public List<CatalogoBloqueDTO>? CatalogoBloques { get; set; }

        public List<MaestroCatalogo>? CatalogoDensidad { get; set; }

        public List<MaestroCatalogo>? CatalogoTipoBloque { get; set; }
    }
}