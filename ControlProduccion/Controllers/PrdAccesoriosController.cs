using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using ControlProduccion.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlProduccion.Controllers
{
    [Authorize]
    public class PrdAccesoriosController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPrdAccesorioService _prdAccesorioService;

        public PrdAccesoriosController(UserManager<IdentityUser> userManager, IPrdAccesorioService prdAccesorioService)
        {
            _userManager = userManager;
            _prdAccesorioService = prdAccesorioService;
        }

        // GET: PrdAccesorios
        public async Task<ActionResult> Index()
        {
            var model = await _prdAccesorioService.GetAllAsync();
            return View(model);
        }

        // GET: PrdAccesorios/Details/5
        [Authorize(Roles = "Supervisor")]
        public async Task<ActionResult> Details(int id)
        {
            var dtoCat = await _prdAccesorioService.GetCreateDataAsync();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var modelDto = await _prdAccesorioService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var vm = MapToViewModel(modelDto, dtoCat, operarios);
            return View(vm);
        }

        // GET: PrdAccesorios/Create
        public async Task<ActionResult> Create()
        {
            var dtoCat = await _prdAccesorioService.GetCreateDataAsync();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var vm = new PrdAccesorioViewModel
            {
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Articulos = dtoCat.CatalogoAccesorios?.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.CodigoArticulo + "-" + a.DescripcionArticulo }),
                TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion }),
                TiposArticulo = dtoCat.CatalogoTipoArticulo?.Select(ta => new SelectListItem { Value = ta.Id.ToString(), Text = ta.Descripcion }),
                MallasCovintec = dtoCat.CatalogoMallasCovintec?.Select(mc => new SelectListItem { Value = mc.Id.ToString(), Text = mc.CodigoArticulo + "-" + mc.DescripcionArticulo }),
                TiposMallaPch = dtoCat.CatalogoTipoMallaPch?.Select(mp => new SelectListItem { Value = mp.Id.ToString(), Text = mp.Cuadricula }),
                AnchosBobina = dtoCat.CatalogoAnchoBobina?.Select(ab => new SelectListItem { Value = ab.Id.ToString(), Text = ab.Valor.ToString() }),
                Calibres = dtoCat.CatalogoCalibre?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Descripcion }),
                Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre })
            };

            return View(vm);
        }

        // POST: PrdAccesorios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromBody] PrdAccesorioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = ModelState });
            }

            var userId = _userManager.GetUserId(User);
            var detalleDto = model.DetPrdAccesorios?.Select(d => new DetPrdAccesorioDto
            {
                IdTipoArticulo = d.IdTipoArticulo,
                IdArticulo = d.IdArticulo,
                IdTipoFabricacion = d.IdTipoFabricacion,
                NumeroPedido = d.NumeroPedido,
                IdMallaCovintec = d.IdMallaCovintec,
                CantidadMallaUn = d.CantidadMallaUn,
                IdTipoMallaPch = d.IdTipoMallaPch,
                CantidadPchKg = d.CantidadPchKg,
                IdAnchoBobina = d.IdAnchoBobina,
                CantidadBobinaKg = d.CantidadBobinaKg,
                IdCalibre = d.IdCalibre,
                CantidadCalibreKg = d.CantidadCalibreKg,
                IdUsuarioCreacion = userId
            }).ToList();

            var dto = new PrdAccesorioDto
            {
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                MermaMallaCovintecKg = model.MermaMallaCovintecKg,
                MermaMallaPchKg = model.MermaMallaPchKg,
                MermaBobinasKg = model.MermaBobinasKg,
                MermaLitewallKg = model.MermaLitewallKg,
                IdUsuarioCreacion = userId,
                DetPrdAccesorios = detalleDto
            };

            await _prdAccesorioService.CreateAsync(dto);
            return Json(new { success = true, message = "Producción guardada!" });
        }

        // GET: PrdAccesorios/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var dtoCat = await _prdAccesorioService.GetCreateDataAsync();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var modelDto = await _prdAccesorioService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var vm = MapToViewModel(modelDto, dtoCat, operarios);
            return View(vm);
        }

        // POST: PrdAccesorios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> Edit([FromBody] PrdAccesorioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = ModelState });
            }

            var userId = _userManager.GetUserId(User);
            var dto = new PrdAccesorioDto
            {
                Id = model.Id,
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                MermaMallaCovintecKg = model.MermaMallaCovintecKg,
                MermaMallaPchKg = model.MermaMallaPchKg,
                MermaBobinasKg = model.MermaBobinasKg,
                MermaLitewallKg = model.MermaLitewallKg,
                IdUsuarioActualizacion = userId
            };

            await _prdAccesorioService.UpdateAsync(dto);
            return Json(new { success = true, message = "Actualización exitosa!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrd( DetPrdAccesorioViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetPrdAccesorioDto
            {
                Id = model.DetPrdAccesorioId,
                PrdAccesoriosId = model.PrdAccesoriosId,
                IdTipoArticulo = model.IdTipoArticulo,
                IdArticulo = model.IdArticulo,
                IdTipoFabricacion = model.IdTipoFabricacion,
                NumeroPedido = model.NumeroPedido,
                IdMallaCovintec = model.IdMallaCovintec,
                CantidadMallaUn = model.CantidadMallaUn,
                IdTipoMallaPch = model.IdTipoMallaPch,
                CantidadPchKg = model.CantidadPchKg,
                IdAnchoBobina = model.IdAnchoBobina,
                CantidadBobinaKg = model.CantidadBobinaKg,
                IdCalibre = model.IdCalibre,
                CantidadCalibreKg = model.CantidadCalibreKg,
              
                IdUsuarioActualizacion = userId
            };

            await _prdAccesorioService.UpdateDetPrdAsync(dto);
            return Json(new { success = true, message = "Actualización exitosa!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> DeleteDetPrd([FromBody] DetPrdAccesorioViewModel model)
        {
            // Note: For this initial implementation, we'll assume the service has this method
            // In a future update, we should add the DeleteDetPrd method to the service
            return Json(new { success = true, message = "Funcionalidad pendiente de implementar!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> ValidarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdAccesorioService.ValidatePrdAccesorioByIdAsync(id, userId);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> AprobarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdAccesorioService.AprovePrdAccesorioByIdAsync(id, userId);
            return Json(result);
        }

        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> GetDataReport(DateTime? start, DateTime? end)
        {
            // Validate and ensure DateTime parameters are within valid SQL Server range
            var startDate = start.HasValue ? ValidateDateTimeParameter(start.Value) : DateTime.Today.AddDays(-7);
            var endDate = end.HasValue ? ValidateDateTimeParameter(end.Value) : DateTime.Today;

            var data = _prdAccesorioService.GetAllWithDetailsAsync(startDate, endDate).Result;
            return View(data);
        }

        /// <summary>
        /// Validates DateTime parameters to ensure they're within SQL Server's valid range
        /// and provides sensible defaults for invalid values
        /// </summary>
        /// <param name="dateTime">DateTime to validate</param>
        /// <returns>Valid DateTime within SQL Server range</returns>
        private static DateTime ValidateDateTimeParameter(DateTime dateTime)
        {
            // SQL Server DateTime range: 1/1/1753 12:00:00 AM to 12/31/9999 11:59:59 PM
            var sqlMinDate = new DateTime(1753, 1, 1);
            var sqlMaxDate = new DateTime(9999, 12, 31, 23, 59, 59);

            // If the date is outside the valid range or is DateTime.MinValue, return a sensible default
            if (dateTime < sqlMinDate || dateTime == DateTime.MinValue)
                return DateTime.Today.AddDays(-1); // Yesterday as default start

            if (dateTime > sqlMaxDate)
                return DateTime.Today; // Today as default end

            return dateTime;
        }

        private PrdAccesorioViewModel MapToViewModel(PrdAccesorioDto modelDto, CrearPrdAccesorioDto dtoCat, IEnumerable<IdentityUser> operarios)
        {
            var articulosDict = dtoCat.CatalogoAccesorios?.GroupBy(a => a.Id).ToDictionary(g => g.Key, g => g.First());
            var tiposFabDict = dtoCat.CatTipoFabricacion?.GroupBy(t => t.Id).ToDictionary(g => g.Key, g => g.First().Descripcion);
            var tiposArticuloDict = dtoCat.CatalogoTipoArticulo?.GroupBy(ta => ta.Id).ToDictionary(g => g.Key, g => g.First().Descripcion);
            var mallasCovintecDict = dtoCat.CatalogoMallasCovintec?.GroupBy(mc => mc.Id).ToDictionary(g => g.Key, g => g.First().CodigoArticulo + "-" + g.First().DescripcionArticulo);
            var tiposMallaPchDict = dtoCat.CatalogoTipoMallaPch?.GroupBy(mp => mp.Id).ToDictionary(g => g.Key, g => g.First().Cuadricula);
            var anchosBobinaDict = dtoCat.CatalogoAnchoBobina?.GroupBy(ab => ab.Id).ToDictionary(g => g.Key, g => g.First().Valor.ToString());
            var calibresDict = dtoCat.CatalogoCalibre?.GroupBy(c => c.Id).ToDictionary(g => g.Key, g => g.First().Descripcion);
            var maquinaDict = dtoCat.CatMaquina?.GroupBy(m => m.Id).ToDictionary(g => g.Key, g => g.First().Nombre);

            return new PrdAccesorioViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                Fecha = modelDto.Fecha,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                IdAprobadoSupervisor = modelDto.IdAprobadoSupervisor,
                IdAprobadoGerencia = modelDto.IdAprobadoGerencia,
                IdMaquina = modelDto.IdMaquina,
                Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Articulos = dtoCat.CatalogoAccesorios?.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.CodigoArticulo + "-" + a.DescripcionArticulo }),
                TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion }),
                TiposArticulo = dtoCat.CatalogoTipoArticulo?.Select(ta => new SelectListItem { Value = ta.Id.ToString(), Text = ta.Descripcion }),
                MallasCovintec = dtoCat.CatalogoMallasCovintec?.Select(mc => new SelectListItem { Value = mc.Id.ToString(), Text = mc.CodigoArticulo + "-" + mc.DescripcionArticulo }),
                TiposMallaPch = dtoCat.CatalogoTipoMallaPch?.Select(mp => new SelectListItem { Value = mp.Id.ToString(), Text = mp.Cuadricula }),
                AnchosBobina = dtoCat.CatalogoAnchoBobina?.Select(ab => new SelectListItem { Value = ab.Id.ToString(), Text = ab.Valor.ToString() }),
                Calibres = dtoCat.CatalogoCalibre?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Descripcion }),
                Observaciones = modelDto.Observaciones,
                MermaMallaCovintecKg = modelDto.MermaMallaCovintecKg,
                MermaMallaPchKg = modelDto.MermaMallaPchKg,
                MermaBobinasKg = modelDto.MermaBobinasKg,
                MermaLitewallKg = modelDto.MermaLitewallKg,
                DetPrdAccesorios = modelDto.DetPrdAccesorios?.Select(d => new DetPrdAccesorioViewModel
                {
                    DetPrdAccesorioId = d.Id,
                    PrdAccesoriosId = d.PrdAccesoriosId,
                    IdTipoArticulo = d.IdTipoArticulo,
                    TipoArticulo = tiposArticuloDict?.GetValueOrDefault(d.IdTipoArticulo),
                    IdArticulo = d.IdArticulo,
                    Articulo = articulosDict != null && articulosDict.ContainsKey(d.IdArticulo)
                        ? $"{articulosDict[d.IdArticulo].CodigoArticulo}-{articulosDict[d.IdArticulo].DescripcionArticulo}"
                        : null,
                    IdTipoFabricacion = d.IdTipoFabricacion,
                    TipoFabricacion = tiposFabDict?.GetValueOrDefault(d.IdTipoFabricacion),
                    NumeroPedido = d.NumeroPedido,
                    IdMallaCovintec = d.IdMallaCovintec,
                    MallaCovintec = mallasCovintecDict?.GetValueOrDefault(d.IdMallaCovintec ?? 0),
                    CantidadMallaUn = d.CantidadMallaUn,
                    IdTipoMallaPch = d.IdTipoMallaPch,
                    TipoMallaPch = tiposMallaPchDict?.GetValueOrDefault(d.IdTipoMallaPch ?? 0),
                    CantidadPchKg = d.CantidadPchKg,
                    IdAnchoBobina = d.IdAnchoBobina,
                    AnchoBobina = anchosBobinaDict?.GetValueOrDefault(d.IdAnchoBobina ?? 0),
                    CantidadBobinaKg = d.CantidadBobinaKg,
                    IdCalibre = d.IdCalibre,
                    Calibre = calibresDict?.GetValueOrDefault(d.IdCalibre ?? 0),
                    CantidadCalibreKg = d.CantidadCalibreKg
               
                }).ToList()
            };
        }

        [HttpGet]
        public async Task<JsonResult> GetArticulosByTipo(int idTipoArticulo)
        {
            var articulos = await _prdAccesorioService.GetCatalogoAccesoriosByTipoAsync(idTipoArticulo);
            var result = articulos.Select(a => new { id = a.Id, texto = $"{a.CodigoArticulo}-{a.DescripcionArticulo}" });
            return Json(result);
        }
    }
}