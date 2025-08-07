using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class LineaProduccion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<CatalogoCercha> CatalogoCerchas { get; set; } = new List<CatalogoCercha>();

    public virtual ICollection<CatalogoMallasCovintec> CatalogoMallasCovintecs { get; set; } = new List<CatalogoMallasCovintec>();

    public virtual ICollection<CatalogoPanelesCovintec> CatalogoPanelesCovintecs { get; set; } = new List<CatalogoPanelesCovintec>();

    public virtual ICollection<Reporte> Reportes { get; set; } = new List<Reporte>();
}
