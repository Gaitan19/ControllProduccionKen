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
    public class PrdCortePController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPrdCortePService _prdCortePService;

        public PrdCortePController(UserManager<IdentityUser> userManager, IPrdCortePService prdCortePService)
        {
            _userManager = userManager;
            _prdCortePService = prdCortePService;
        }

        // GET: PrdCorteP
        public async Task<ActionResult> Index()
        {
            var model = await _prdCortePService.GetAllAsync();
            return View(model);
        }

        // GET: PrdCorteP/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var dtoCat = await _prdCortePService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var modelDto = await _prdCortePService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var vm = MapToViewModel(modelDto, dtoCat, operarios);
            return View(vm);
        }

        // GET: PrdCorteP/Create
        public async Task<ActionResult> Create()
        {
            var dtoCat = await _prdCortePService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var vm = new PrdCortePViewModel
            {
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Articulos = dtoCat.CatPantografo?.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.CodigoArticulo + "-" + a.DescripcionArticulo }),
                TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion }),
                CatalogoDensidad = dtoCat.CatalogoDensidad?.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Descripcion }),
                CatalogoTipoBloque = dtoCat.CatalogoTipoBloque?.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Descripcion }),
                Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                subDetalleBloqueCodigo= dtoCat.SubDetPrdBloqueCodigo?.Select(b => new SelectListItem { Value = b, Text = b })

            };

            return View(vm);
        }

        // POST: PrdCorteP/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] PrdCortePViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _userManager.GetUserId(User);
            model.IdUsuarioCreacion = userId;

            var detalleDto = model.DetPrdCorteP?.Select(d => new DetPrdCortePDTO
            {
                No = d.No,
                IdArticulo = d.IdArticulo,
                IdTipoFabricacion = d.IdTipoFabricacion,
                NumeroPedido = d.NumeroPedido,
                PrdCodigoBloque = d.PrdCodigoBloque,
                IdDensidad = d.IdDensidad,
                IdTipoBloque = d.IdTipoBloque,
                CantidadPiezasConformes = d.CantidadPiezasConformes,
                CantidadPiezasNoConformes = d.CantidadPiezasNoConformes,
                Nota = d.Nota,
                IdUsuarioCreacion = userId
            }).ToList();

            var dto = new PrdCortePDTO
            {
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                TiempoParo = model.TiempoParo,
                IdUsuarioCreacion = model.IdUsuarioCreacion,
                DetPrdCortePs = detalleDto
            };

            await _prdCortePService.CreateAsync(dto);
            return Json(new { success = true, message = "Producción guardada!" });
        }

        // GET: PrdCorteP/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var dtoCat = await _prdCortePService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var modelDto = await _prdCortePService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var vm = MapToViewModel(modelDto, dtoCat, operarios);
            return View(vm);
        }

        // POST: PrdCorteP/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> Edit(PrdCortePViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var dtoCat = await _prdCortePService.GetCreateData();
                var operarios = await _userManager.GetUsersInRoleAsync("Operario");

                model.Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName });
                model.Articulos = dtoCat.CatPantografo?.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.CodigoArticulo + "-" + a.DescripcionArticulo });
                model.TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion });
                model.CatalogoDensidad = dtoCat.CatalogoDensidad?.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Descripcion });
                model.CatalogoTipoBloque = dtoCat.CatalogoTipoBloque?.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Descripcion });
                model.Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre });
                model.subDetalleBloqueCodigo = dtoCat.SubDetPrdBloqueCodigo?.Select(b => new SelectListItem { Value = b, Text = b });

                return View(model);
            }

            var userId = _userManager.GetUserId(User);
            var dto = new PrdCortePDTO
            {
                Id = model.Id,
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                TiempoParo = model.TiempoParo,
                IdUsuarioActualizacion = userId
            };

            await _prdCortePService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrd(DetPrdCortePViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetPrdCortePDTO
            {
                Id = model.DetPrdCortePId,
                PrdCortePid = model.PrdCortePid,
                No = model.No,
                IdArticulo = model.IdArticulo,
                IdTipoFabricacion = model.IdTipoFabricacion,
                NumeroPedido = model.NumeroPedido,
                PrdCodigoBloque = model.PrdCodigoBloque,
                IdDensidad = model.IdDensidad,
                IdTipoBloque = model.IdTipoBloque,
                CantidadPiezasConformes = model.CantidadPiezasConformes,
                CantidadPiezasNoConformes = model.CantidadPiezasNoConformes,
                Nota = model.Nota,
                IdUsuarioActualizacion = userId
            };

            await _prdCortePService.UpdateDetPrd(dto);
            return Json(new { success = true, message = "Actualización exitosa!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> DeleteDetPrd(DetPrdCortePViewModel model)
        {
            var dto = new DetPrdCortePDTO { Id = model.DetPrdCortePId };
            await _prdCortePService.DeleteDetPrd(dto);
            return Json(new { success = true, message = "Se eliminó registro!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> ValidarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdCortePService.ValidatePrdCortePByIdAsync(id, userId);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> AprobarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdCortePService.AprovePrdCortePByIdAsync(id, userId);
            return Json(result);
        }

        public async Task<ActionResult> GetDataReport(DateTime start, DateTime end)
        {
            // Validate and ensure DateTime parameters are within valid SQL Server range
            start = ValidateDateTimeParameter(start);
            end = ValidateDateTimeParameter(end);
            
            var data = await _prdCortePService.GetAllPrdCortePWithDetailsAsync(start, end);
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

        private PrdCortePViewModel MapToViewModel(PrdCortePDTO modelDto, CrearPrdCortePDTO dtoCat, IEnumerable<IdentityUser> operarios)
        {
            var articulosDict = dtoCat.CatPantografo?.GroupBy(a => a.Id).ToDictionary(g => g.Key, g => g.First());
            var tiposFabDict = dtoCat.CatTipoFabricacion?.GroupBy(t => t.Id).ToDictionary(g => g.Key, g => g.First().Descripcion);
            var densidadDict = dtoCat.CatalogoDensidad?.GroupBy(d => d.Id).ToDictionary(g => g.Key, g => g.First().Descripcion);
            var tipoBloqueDict = dtoCat.CatalogoTipoBloque?.GroupBy(t => t.Id).ToDictionary(g => g.Key, g => g.First().Descripcion);
            var maquinaDict = dtoCat.CatMaquina?.GroupBy(m => m.Id).ToDictionary(g => g.Key, g => g.First().Nombre);
            var subDetalleBloqueCodigoDict = dtoCat.SubDetPrdBloqueCodigo?.Distinct().ToDictionary(b => b, b => b);
            return new PrdCortePViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                Fecha = modelDto.Fecha,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                IdAprobadoSupervisor = modelDto.IdAprobadoSupervisor,
                IdAprobadoGerencia = modelDto.IdAprobadoGerencia,
                NotaSupervisor = modelDto.NotaSupervisor,
                subDetalleBloqueCodigo = subDetalleBloqueCodigoDict?.Select(b => new SelectListItem { Value = b.Key, Text = b.Value }),
                IdMaquina = modelDto.IdMaquina,
                Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Articulos = dtoCat.CatPantografo?.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.CodigoArticulo + "-" + a.DescripcionArticulo }),
                TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion }),
                CatalogoDensidad = dtoCat.CatalogoDensidad?.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Descripcion }),
                CatalogoTipoBloque = dtoCat.CatalogoTipoBloque?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion }),
                Observaciones = modelDto.Observaciones,
                TiempoParo = modelDto.TiempoParo,
                DetPrdCorteP = modelDto.DetPrdCortePs?.Select(d => new DetPrdCortePViewModel
                {
                    DetPrdCortePId = d.Id,
                    PrdCortePid = d.PrdCortePid,
                    No = d.No,
                    IdArticulo = d.IdArticulo,
                    Articulo = articulosDict != null && articulosDict.ContainsKey(d.IdArticulo)
                        ? $"{articulosDict[d.IdArticulo].CodigoArticulo}-{articulosDict[d.IdArticulo].DescripcionArticulo}"
                        : null,
                    IdTipoFabricacion = d.IdTipoFabricacion,
                    NumeroPedido= d.NumeroPedido,
                    TipoFabricacion = tiposFabDict?.GetValueOrDefault(d.IdTipoFabricacion),
                    PrdCodigoBloque = d.PrdCodigoBloque,
                    
                    IdDensidad = d.IdDensidad,
                    Densidad = densidadDict?.GetValueOrDefault(d.IdDensidad),
                    IdTipoBloque = d.IdTipoBloque,
                    TipoBloque = tipoBloqueDict?.GetValueOrDefault(d.IdTipoBloque),
                    CantidadPiezasConformes = d.CantidadPiezasConformes,
                    CantidadPiezasNoConformes = d.CantidadPiezasNoConformes,
                    Nota = d.Nota
                }).ToList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> SaveNotaSupervisor(int id, string notaSupervisor)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdCortePService.UpdateNotaSupervisorAsync(id, notaSupervisor, userId);
            
            if (result)
            {
                return Json(new { success = true, message = "Nota guardada exitosamente" });
            }
            else
            {
                return Json(new { success = false, message = "Error al guardar la nota" });
            }
        }
    }
}

