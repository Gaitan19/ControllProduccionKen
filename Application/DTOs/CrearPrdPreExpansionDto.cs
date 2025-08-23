using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class CrearPrdPreExpansionDto
    {
        /// <summary>
        /// Catálogo de máquinas disponibles para pre-expansión
        /// </summary>
        public List<MaquinaDto>? CatMaquina { get; set; }
        
        /// <summary>
        /// Catálogo de tipos de fabricación
        /// </summary>
        public List<TipoFabricacionDto>? CatTipoFabricacion { get; set; }
      
    }
}