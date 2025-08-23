using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CrearPrdAccesorioDto
    {
        public List<MaquinaDto>? CatMaquina { get; set; }
        public List<TipoFabricacionDto>? CatTipoFabricacion { get; set; }
        public List<CatalogoAccesorio>? CatalogoAccesorios { get; set; }
        public List<MaestroCatalogo>? CatalogoTipoArticulo { get; set; }
        public List<CatalogoMallasCovintec>? CatalogoMallasCovintec { get; set; }
        public List<CatTipoMalla>? CatalogoTipoMallaPch { get; set; }
        public List<AnchoBobina>? CatalogoAnchoBobina { get; set; }
        public List<MaestroCatalogo>? CatalogoCalibre { get; set; }
    }
}