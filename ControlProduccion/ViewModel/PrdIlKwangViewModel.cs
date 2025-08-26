
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControlProduccion.ViewModel
{
    public class PrdIlKwangViewModel : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tipo de Reporte")]
        public int? IdTipoReporte { get; set; }

        [Required(ErrorMessage = "El campo Operador es obligatorio")]
        [Display(Name = "Operador")]
        public List<string> IdUsuarios { get; set; }

        public IEnumerable<SelectListItem>? Operarios { get; set; }

        [Required(ErrorMessage = "El campo M�quina es obligatorio")]
        [Display(Name = "M�quina")]
        public int IdMaquina { get; set; }

        public IEnumerable<SelectListItem>? Maquinas { get; set; }

        [Required(ErrorMessage = "El campo Hora de Inicio es obligatorio")]
        [Display(Name = "Hora de Inicio")]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicio { get; set; } = TimeSpan.FromHours(8);

        [Required(ErrorMessage = "El campo Hora de Fin es obligatorio")]
        [Display(Name = "Hora de Fin")]
        [DataType(DataType.Time)]
        public TimeSpan HoraFin { get; set; } = TimeSpan.FromHours(17);

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Display(Name = "Tiempo de Paro (Horas)")]
        
        public decimal TiempoParo { get; set; }

        [Required(ErrorMessage = "El campo Art�culo es obligatorio")]
        [Display(Name = "Art�culo")]
        public List<string> IdArticulo { get; set; }

        public IEnumerable<SelectListItem>? CatalogoArticulos { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Tipo de Fabricaci�n")]
        public int IdTipoFabricacion { get; set; }

        public IEnumerable<SelectListItem>? TiposFabricacion { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Cliente")]
        public string Cliente { get; set; }

        [Display(Name = "N�mero de Pedido")]
        public int? NumeroPedido { get; set; }

        [Display(Name = "Velocidad de M�quina")]
        public int VelocidadMaquina { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        // Properties for Bobina A
        [Display(Name = "Ubicaci�n Bobina A")]
        public int IdUbicacionBobinaA { get; set; }
        public IEnumerable<SelectListItem>? UbicacionesBobinaA { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Ancho Bobina A")]
        public int IdAnchoBobinaA { get; set; }
        public IEnumerable<SelectListItem>? AnchosBobinaA { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fabricante Bobina A")]
        public string FabricanteBobinaA { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "C�digo Bobina A")]
        public string CodigoBobinaA { get; set; }

        [Display(Name = "Calibre (mm) A")]
        public decimal CalibreMmA { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Color Bobina A")]
        public int IdColorBobinaA { get; set; }
        public IEnumerable<SelectListItem>? ColoresBobinaA { get; set; }

        [Display(Name = "Ancho (mm) A")]
        public int AnchoMmA { get; set; }

        [Display(Name = "Peso Inicial (Kg) A")]
        public int PesoInicialKgA { get; set; }

        [Display(Name = "Peso Final (Kg) A")]
        public int PesoFinalKgA { get; set; }

        [Display(Name = "Metros Inicial A")]
        public int MetrosInicialA { get; set; }

        [Display(Name = "Metros Final A")]
        public int MetrosFinalA { get; set; }

        [Display(Name = "Espesor Inicial (cm) A")]
        public int EspesorInicialCmA { get; set; }

        [Display(Name = "Espesor Final (cm) A")]
        public int EspesorFinalCmA { get; set; }

        // Properties for Bobina B
        [Display(Name = "Ubicaci�n Bobina B")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int IdUbicacionBobinaB { get; set; }
        public IEnumerable<SelectListItem>? UbicacionesBobinaB { get; set; }

        [Display(Name = "Ancho Bobina B")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int IdAnchoBobinaB { get; set; }
        public IEnumerable<SelectListItem>? AnchosBobinaB { get; set; }

        [Display(Name = "Fabricante Bobina B")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string FabricanteBobinaB { get; set; }

        [Display(Name = "C�digo Bobina B")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string CodigoBobinaB { get; set; }

        [Display(Name = "Calibre (mm) B")]
        public decimal CalibreMmB { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Color Bobina B")]
        public int IdColorBobinaB { get; set; }
        public IEnumerable<SelectListItem>? ColoresBobinaB { get; set; }

        [Display(Name = "Ancho (mm) B")]
        public int AnchoMmB { get; set; }

        [Display(Name = "Peso Inicial (Kg) B")]
        public int PesoInicialKgB { get; set; }

        [Display(Name = "Peso Final (Kg) B")]
        public int PesoFinalKgB { get; set; }

        [Display(Name = "Metros Inicial B")]
        public int MetrosInicialB { get; set; }

        [Display(Name = "Metros Final B")]
        public int MetrosFinalB { get; set; }

        [Display(Name = "Espesor Inicial (cm) B")]
        public int EspesorInicialCmB { get; set; }

        [Display(Name = "Espesor Final (cm) B")]
        public int EspesorFinalCmB { get; set; }

        // Poliol / Isocianato
        [Display(Name = "Peso Inicial A")]
        public decimal PesoInicialA { get; set; }

        [Display(Name = "Cantidad Utilizada A")]
        public decimal CantidadUtilizadaA { get; set; }

        [Display(Name = "Peso Final A")]
        public decimal PesoFinalA { get; set; }

        [Display(Name = "Velocidad Superior A")]
        public decimal VelocidadSuperiorA { get; set; }

        [Display(Name = "Velocidad Inferior A")]
        public decimal VelocidadInferiorA { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Lote A")]
        public string LoteA { get; set; }

        [Display(Name = "Vencimiento A")]
        [DataType(DataType.Date)]
        public DateTime VencimientoA { get; set; } = DateTime.Now;

        [Display(Name = "Peso Inicial B")]
        public decimal PesoInicialB { get; set; }

        [Display(Name = "Cantidad Utilizada B")]
        public decimal CantidadUtilizadaB { get; set; }

        [Display(Name = "Peso Final B")]
        public decimal PesoFinalB { get; set; }

        [Display(Name = "Velocidad Superior B")]
        public decimal VelocidadSuperiorB { get; set; }

        [Display(Name = "Velocidad Inferior B")]
        public decimal VelocidadInferiorB { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Lote B")]
        public string LoteB { get; set; }
       
        [Display(Name = "Vencimiento B")]
        [DataType(DataType.Date)]
        public DateTime VencimientoB { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string Observaciones { get; set; }

        [Display(Name = "Cantidad de Arranques")]
        public int CantidadArranques { get; set; }
        [Display(Name = "Metros Inicial Poliol")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int MetrosInicialPoliol { get; set; }

        [Display(Name = "Metros Inicial Isocianato")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int MetrosInicialIsocianato { get; set; }
        public string? IdUsuarioCreacion { get; set; } = string.Empty;
        public DateTime? FechaCreacion { get; set; } = DateTime.Now;
        public string? IdUsuarioActualizacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        public bool? AprobadoSupervisor { get; set; }
        public bool? AprobadoGerencia { get; set; }

        [Display(Name = "Nota Supervisor")]
        [DataType(DataType.MultilineText)]
        public string? NotaSupervisor { get; set; }

        public List<DetPrdIlKwangViewModel>? DetPrdIlKwangs { get; set; }

        // Catalogos para los detalles
        public IEnumerable<SelectListItem>? CatEspesor { get; set; }
        public IEnumerable<SelectListItem>? CatStatus { get; set; }
        public IEnumerable<SelectListItem>? CatTipo { get; set; }

        // Aqu� va la validaci�n cruzada:
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 1. Hora
            if (HoraFin < HoraInicio)
                yield return new ValidationResult(
                    "La Hora de Fin no puede ser menor que la Hora de Inicio.",
                    new[] { nameof(HoraFin) });

            // 2. N�meros: final > inicial dispara error
            if (PesoFinalKgA > PesoInicialKgA)
                yield return new ValidationResult(
                    "El Peso Final (Kg) A no puede ser mayor que el Peso Inicial (Kg) A.",
                    new[] { nameof(PesoFinalKgA) });

            if (MetrosFinalA > MetrosInicialA)
                yield return new ValidationResult(
                    "Los Metros Finales A no pueden ser mayores que los Metros Iniciales A.",
                    new[] { nameof(MetrosFinalA) });

            if (EspesorFinalCmA > EspesorInicialCmA)
                yield return new ValidationResult(
                    "El Espesor Final (cm) A no puede ser mayor que el Inicial.",
                    new[] { nameof(EspesorFinalCmA) });

            if (PesoFinalKgB > PesoInicialKgB)
                yield return new ValidationResult(
                    "El Peso Final (Kg) B no puede ser mayor que el Peso Inicial (Kg) B.",
                    new[] { nameof(PesoFinalKgB) });

            if (MetrosFinalB > MetrosInicialB)
                yield return new ValidationResult(
                    "Los Metros Finales B no pueden ser mayores que los Metros Iniciales B.",
                    new[] { nameof(MetrosFinalB) });

            if (EspesorFinalCmB > EspesorInicialCmB)
                yield return new ValidationResult(
                    "El Espesor Final (cm) B no puede ser mayor que el Inicial.",
                    new[] { nameof(EspesorFinalCmB) });

            if (PesoFinalA > PesoInicialA)
                yield return new ValidationResult(
                    "El Peso Final A no puede ser mayor que el Peso Inicial A.",
                    new[] { nameof(PesoFinalA) });

            if (PesoFinalB > PesoInicialB)
                yield return new ValidationResult(
                    "El Peso Final B no puede ser mayor que el Peso Inicial B.",
                    new[] { nameof(PesoFinalB) });
        }
    }
}
