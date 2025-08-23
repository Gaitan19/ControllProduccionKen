using Application.DTOs;
using Application.Interfaces;
using ControlProduccion.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlProduccion.Controllers
{
    [Authorize]
    public class PrdPaneladoraPchController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPrdPaneladoraPchService _prdPaneladoraPchService;

        public PrdPaneladoraPchController(UserManager<IdentityUser> userManager, IPrdPaneladoraPchService prdPaneladoraPchService)
        {
            _userManager = userManager;
            _prdPaneladoraPchService = prdPaneladoraPchService;
        }

        // GET: PrdPaneladoraPch
        public async Task<ActionResult> Index()
        {
            var model = await _prdPaneladoraPchService.GetAllAsync();
            return View(model);
        }

        // GET: PrdPaneladoraPch/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var dtoCat = await _prdPaneladoraPchService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var modelDto = await _prdPaneladoraPchService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var vm = MapToViewModel(modelDto, dtoCat, operarios);
            return View(vm);
        }

        // GET: PrdPaneladoraPch/Create
        public async Task<ActionResult> Create()
        {
            var dtoCat = await _prdPaneladoraPchService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var vm = new PrdPaneladoraPchViewModel
            {
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion }),
                CatalogoPaneles = dtoCat.CatalogoPanelesPch?.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.CodigoArticulo + "-" + p.DescripcionArticulo })
            };

            return View(vm);
        }

        // POST: PrdPaneladoraPch/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] PrdPaneladoraPchViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _userManager.GetUserId(User);
            model.IdUsuarioCreacion = userId;

            var detalleDto = model.DetPrdPaneladoraPch?.Select(d => new DetPrdPaneladoraPchDTO
            {
                IdArticulo = d.IdArticulo,
                Longitud = d.Longitud,
                CantidadProducida = d.CantidadProducida,
                CantidadNoConforme = d.CantidadNoConforme,
             
                IdTipoFabricacion = d.IdTipoFabricacion,
                NumeroPedido = d.NumeroPedido,
                NumeroAlambre = d.NumeroAlambre,
                PesoAlambreKg = d.PesoAlambreKg,
                MermaAlambreKg = d.MermaAlambreKg,
                IdUsuarioCreacion = userId
            }).ToList();

            var dto = new PrdPaneladoraPchDTO
            {
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                TiempoParo = model.TiempoParo,
                IdUsuarioCreacion = model.IdUsuarioCreacion,
                DetPrdPaneladoraPches = detalleDto
            };

            await _prdPaneladoraPchService.CreateAsync(dto);
            return Json(new { success = true, message = "Producción guardada!" });
        }

        // GET: PrdPaneladoraPch/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var dtoCat = await _prdPaneladoraPchService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var modelDto = await _prdPaneladoraPchService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var vm = MapToViewModel(modelDto, dtoCat, operarios);
            return View(vm);
        }

        // POST: PrdPaneladoraPch/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> Edit(PrdPaneladoraPchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var dtoCat = await _prdPaneladoraPchService.GetCreateData();
                var operarios = await _userManager.GetUsersInRoleAsync("Operario");

                model.Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName });
                model.Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre });
                model.TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion });
                model.CatalogoPaneles = dtoCat.CatalogoPanelesPch?.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.CodigoArticulo + "-" + p.DescripcionArticulo });

                return View(model);
            }

            var userId = _userManager.GetUserId(User);
            var dto = new PrdPaneladoraPchDTO
            {
                Id = model.Id,
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                TiempoParo = model.TiempoParo,
                IdUsuarioActualizacion = userId
            };

            await _prdPaneladoraPchService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrd(DetPrdPaneladoraPchViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetPrdPaneladoraPchDTO
            {
                Id = model.DetPrdPaneladoraPchId,
                PrdPaneladoraPchId = model.PrdPaneladoraPchId,
                IdArticulo = model.IdArticulo,
                Longitud = model.Longitud,
                CantidadProducida = model.CantidadProducida,
                CantidadNoConforme = model.CantidadNoConforme,
              
                IdTipoFabricacion = model.IdTipoFabricacion,
                NumeroPedido = model.NumeroPedido,
                NumeroAlambre = model.NumeroAlambre,
                PesoAlambreKg = model.PesoAlambreKg,
                MermaAlambreKg = model.MermaAlambreKg,
                IdUsuarioActualizacion = userId
            };

            await _prdPaneladoraPchService.UpdateDetPrd(dto);
            return Json(new { success = true, message = "Actualización exitosa!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> DeleteDetPrd(DetPrdPaneladoraPchViewModel model)
        {
            var dto = new DetPrdPaneladoraPchDTO { Id = model.DetPrdPaneladoraPchId };
            await _prdPaneladoraPchService.DeleteDetPrd(dto);
            return Json(new { success = true, message = "Se eliminó registro!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> ValidarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdPaneladoraPchService.ValidatePrdPaneladoraPchByIdAsync(id, userId);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> AprobarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdPaneladoraPchService.AprovePrdPaneladoraPchByIdAsync(id, userId);
            return Json(result);
        }

        public async Task<ActionResult> GetDataReport(DateTime start, DateTime end)
        {
            // Validate and ensure DateTime parameters are within valid SQL Server range
            start = ValidateDateTimeParameter(start);
            end = ValidateDateTimeParameter(end);
            
            var data = await _prdPaneladoraPchService.GetAllPrdPaneladoraPchWithDetailsAsync(start, end);
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

        private PrdPaneladoraPchViewModel MapToViewModel(PrdPaneladoraPchDTO modelDto, CrearPrdPaneladoraPchDTO dtoCat, IEnumerable<IdentityUser> operarios)
        {
            var maquinaDict = dtoCat.CatMaquina?.GroupBy(m => m.Id).ToDictionary(g => g.Key, g => g.First().Nombre);
            var tiposFabDict = dtoCat.CatTipoFabricacion?.GroupBy(t => t.Id).ToDictionary(g => g.Key, g => g.First().Descripcion);
            var panelesDict = dtoCat.CatalogoPanelesPch?.GroupBy(p => p.Id).ToDictionary(g => g.Key, g => g.First());

            return new PrdPaneladoraPchViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                IdMaquina = modelDto.IdMaquina,
                Fecha = modelDto.Fecha,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                IdAprobadoSupervisor = modelDto.IdAprobadoSupervisor,
                IdAprobadoGerencia = modelDto.IdAprobadoGerencia,
                Observaciones = modelDto.Observaciones,
                TiempoParo = modelDto.TiempoParo,
                ProduccionDia = modelDto.ProduccionDia,
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion }),
                CatalogoPaneles = dtoCat.CatalogoPanelesPch?.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.CodigoArticulo + "-" + p.DescripcionArticulo }),
                DetPrdPaneladoraPch = modelDto.DetPrdPaneladoraPches?.Select(d => new DetPrdPaneladoraPchViewModel
                {
                    DetPrdPaneladoraPchId = d.Id,
                    PrdPaneladoraPchId = d.PrdPaneladoraPchId,
                    IdArticulo = d.IdArticulo,
                    Articulo = panelesDict != null && panelesDict.ContainsKey(d.IdArticulo)
                        ? $"{panelesDict[d.IdArticulo].CodigoArticulo}-{panelesDict[d.IdArticulo].DescripcionArticulo}"
                        : null,
                    Longitud = d.Longitud,
                    CantidadProducida = d.CantidadProducida,
                    CantidadNoConforme = d.CantidadNoConforme,
                    Mts2PorPanel = d.Mts2PorPanel,
                    IdTipoFabricacion = d.IdTipoFabricacion,
                    TipoFabricacion = tiposFabDict?.GetValueOrDefault(d.IdTipoFabricacion),
                    NumeroPedido = d.NumeroPedido,
                    NumeroAlambre = d.NumeroAlambre,
                    PesoAlambreKg = d.PesoAlambreKg,
                    MermaAlambreKg = d.MermaAlambreKg
                }).ToList()
            };
        }
    }
}