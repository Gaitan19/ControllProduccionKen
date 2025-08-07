using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CrearPrdCorteTDTO
    {
        public List<MaquinaDto>? CatMaquina { get; set; }

        public List<TipoFabricacionDto>? CatTipoFabricacion { get; set; }

        public List<string>? SubDetPrdBloqueCodigo { get; set; }

        public List<MaestroCatalogo>? CatalogoDensidad { get; set; }

        public List<MaestroCatalogo>? CatalogoTipoBloque { get; set; }
        public List<CatLamina>? CatLamina { get; set; }
    }
}
