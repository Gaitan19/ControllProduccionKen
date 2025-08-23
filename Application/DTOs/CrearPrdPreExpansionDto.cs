using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class CrearPrdPreExpansionDto
    {
        /// <summary>
        /// Cat�logo de m�quinas disponibles para pre-expansi�n
        /// </summary>
        public List<MaquinaDto>? CatMaquina { get; set; }
        
        /// <summary>
        /// Cat�logo de tipos de fabricaci�n
        /// </summary>
        public List<TipoFabricacionDto>? CatTipoFabricacion { get; set; }
      
    }
}