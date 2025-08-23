using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class TipoFabricacion
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<DetPrdAccesorio> DetPrdAccesorios { get; set; } = new List<DetPrdAccesorio>();

    public virtual ICollection<DetPrdCerchaCovintec> DetPrdCerchaCovintecs { get; set; } = new List<DetPrdCerchaCovintec>();

    public virtual ICollection<DetPrdMallaCovintec> DetPrdMallaCovintecs { get; set; } = new List<DetPrdMallaCovintec>();

    public virtual ICollection<DetPrdNevera> DetPrdNeveras { get; set; } = new List<DetPrdNevera>();

    public virtual ICollection<DetPrdOtro> DetPrdOtros { get; set; } = new List<DetPrdOtro>();

    public virtual ICollection<DetPrdPaneladoraPch> DetPrdPaneladoraPches { get; set; } = new List<DetPrdPaneladoraPch>();

    public virtual ICollection<DetPrdPanelesCovintec> DetPrdPanelesCovintecs { get; set; } = new List<DetPrdPanelesCovintec>();

    public virtual ICollection<DetPrdPchMaquinaA> DetPrdPchMaquinaAs { get; set; } = new List<DetPrdPchMaquinaA>();

    public virtual ICollection<DetPrdPchMaquinaB> DetPrdPchMaquinaBs { get; set; } = new List<DetPrdPchMaquinaB>();

    public virtual ICollection<DetPrdPchMaquinaC> DetPrdPchMaquinaCs { get; set; } = new List<DetPrdPchMaquinaC>();

    public virtual ICollection<PrdIlKwang> PrdIlKwangs { get; set; } = new List<PrdIlKwang>();

    public virtual ICollection<SubDetPrdBloque> SubDetPrdBloques { get; set; } = new List<SubDetPrdBloque>();
}
