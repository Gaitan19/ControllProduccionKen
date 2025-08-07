using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class DetPrdBloque
{
    public int Id { get; set; }

    public int PrdBloqueId { get; set; }

    public int IdMaquina { get; set; }

    public int IdCatBloque { get; set; }

    public decimal? ProduccionDia { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string IdUsuarioCreacion { get; set; } = null!;

    public DateTime? FechaActualizacion { get; set; }

    public string? IdUsuarioActualizacion { get; set; }

    public virtual CatalogoBloque IdCatBloqueNavigation { get; set; } = null!;

    public virtual Maquina IdMaquinaNavigation { get; set; } = null!;

    public virtual PrdBloque PrdBloque { get; set; } = null!;

    public virtual ICollection<SubDetPrdBloque> SubDetPrdBloques { get; set; } = new List<SubDetPrdBloque>();
}
