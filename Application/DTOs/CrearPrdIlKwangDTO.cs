using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CrearPrdIlKwangDTO
    {
        public List<MaquinaDto>? CatMaquina { get; set; }

        public List<TipoFabricacionDto>? CatTipoFabricacion { get; set; }

      public List<CatProdTermoIsoPanelDTO>? CatTermoIsoPanel { get; set; }
        public List<AnchoBobinaDTO>? CatAnchoBobina { get; set; }
        public List<ColoresBobinaDTO>? CatColoresBobina { get; set; }
       
       
        public List<UbicacionBobinaDTO>? CatUbicacionBobina { get; set; }
        public List<CatEspesorDTO>? CatEspesor { get; set; }
        public List<CatalogoTipoDTO>? CatTipo { get; set; }
        public List<CatalogoStatusDTO>? CatStatus { get; set; }

    }
}
