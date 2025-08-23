using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Maquina
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<DetPrdBloque> DetPrdBloques { get; set; } = new List<DetPrdBloque>();

    public virtual ICollection<DetPrdPchMaquinaA> DetPrdPchMaquinaAs { get; set; } = new List<DetPrdPchMaquinaA>();

    public virtual ICollection<DetPrdPchMaquinaB> DetPrdPchMaquinaBs { get; set; } = new List<DetPrdPchMaquinaB>();

    public virtual ICollection<DetPrdPchMaquinaC> DetPrdPchMaquinaCs { get; set; } = new List<DetPrdPchMaquinaC>();

    public virtual ICollection<PrdAccesorio> PrdAccesorios { get; set; } = new List<PrdAccesorio>();

    public virtual ICollection<PrdCerchaCovintec> PrdCerchaCovintecs { get; set; } = new List<PrdCerchaCovintec>();

    public virtual ICollection<PrdCorteP> PrdCortePs { get; set; } = new List<PrdCorteP>();

    public virtual ICollection<PrdCorteT> PrdCorteTs { get; set; } = new List<PrdCorteT>();

    public virtual ICollection<PrdIlKwang> PrdIlKwangs { get; set; } = new List<PrdIlKwang>();

    public virtual ICollection<PrdMallaCovintec> PrdMallaCovintecs { get; set; } = new List<PrdMallaCovintec>();

    public virtual ICollection<PrdNevera> PrdNeveras { get; set; } = new List<PrdNevera>();

    public virtual ICollection<PrdPaneladoraPch> PrdPaneladoraPches { get; set; } = new List<PrdPaneladoraPch>();

    public virtual ICollection<PrdPanelesCovintec> PrdPanelesCovintecs { get; set; } = new List<PrdPanelesCovintec>();
}
