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
    public class PrdBloquesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPrdBloquesService _prdBloquesService;

        public PrdBloquesController(UserManager<IdentityUser> userManager, IPrdBloquesService prdBloquesService)
        {
            _userManager = userManager;
            _prdBloquesService = prdBloquesService;
        }

        // GET: PrdBloques
        public async Task<ActionResult> Index()
        {
            var model = await _prdBloquesService.GetAllAsync();
            return View(model);
        }

        // GET: PrdBloques/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var dtoCat = await _prdBloquesService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var modelDto = await _prdBloquesService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var vm = MapToViewModel(modelDto, dtoCat, operarios);

            return View(vm);
        }

        // GET: PrdBloques/Create
        public async Task<ActionResult> Create()
        {
            var dto = await _prdBloquesService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var vm = new PrdBloqueViewModel
            {
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Maquinas = dto.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                CatalogoBloques = dto.CatalogoBloques?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Bloque+" - "+c.Medidas }),
                CatalogoDensidad = dto.CatalogoDensidad?.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Descripcion }),
                CatalogoTipoBloque = dto.CatalogoTipoBloque?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion }),
                TiposFabricacion = dto.CatTipoFabricacion?.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
            };

            return View(vm);
        }

        // POST: PrdBloques/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] PrdBloqueViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _userManager.GetUserId(User);
            model.IdUsuarioCreacion = userId;

            var detalleDto = model.DetPrdBloques?.Select(d => new DetPrdBloqueDTO
            {
                IdMaquina = d.IdMaquina,
                IdCatBloque = d.IdCatBloque,
                IdUsuarioCreacion = userId,
                SubDetPrdBloques = d.SubDetPrdBloques?.Select(s => new SubDetPrdBloqueDTO
                {
                    IdArticulo = s.IdArticulo,
                    No = s.No,
                    Hora = s.Hora,
                    Silo = s.Silo,
                    IdDensidad = s.IdDensidad,
                    IdTipoBloque = s.IdTipoBloque,
                    Peso = s.Peso,
                    IdTipoFabricacion = s.IdTipoFabricacion,
                    NumeroPedido = s.NumeroPedido,
                    CodigoBloque = s.CodigoBloque,
                    Observaciones = s.Observaciones,
                    IdUsuarioCreacion = userId
                }).ToList()
            }).ToList();

            var dto = new PrdBloqueDTO
            {
                IdUsuarios = model.IdUsuarios,
                Fecha = model.Fecha,
                IdUsuarioCreacion = model.IdUsuarioCreacion,
                DetPrdBloques = detalleDto
            };

            await _prdBloquesService.CreateAsync(dto);
            return Json(new { success = true, message = "Producción guardada!" });
        }

        // GET: PrdBloques/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var dtoCat = await _prdBloquesService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");
            var modelDto = await _prdBloquesService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var vm = MapToViewModel(modelDto, dtoCat, operarios);

            return View(vm);
        }

        // POST: PrdBloques/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> Edit(PrdBloqueViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var dtoCat = await _prdBloquesService.GetCreateData();
                var operarios = await _userManager.GetUsersInRoleAsync("Operario");

                model.Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName });
                model.Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre });
                model.CatalogoBloques = dtoCat.CatalogoBloques?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Bloque });
                model.CatalogoDensidad = dtoCat.CatalogoDensidad?.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Descripcion });
                model.CatalogoTipoBloque = dtoCat.CatalogoTipoBloque?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion });
                model.TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion });

                return View(model);
            }

            var userId = _userManager.GetUserId(User);
            var dto = new PrdBloqueDTO
            {
                Id = model.Id,
                IdUsuarios = model.IdUsuarios,
                Fecha = model.Fecha,
                IdUsuarioActualizacion = userId
            };

            await _prdBloquesService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrd(DetPrdBloqueViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetPrdBloqueDTO
            {
               Id=(int)model.detrpdId,
                PrdBloqueId= model.PrdBloqueId,
                IdMaquina = model.IdMaquina,
                IdCatBloque = model.IdCatBloque,
                IdUsuarioActualizacion = userId
            };

            await _prdBloquesService.UpdateDetPrd(dto);
            return Json(new { success = true, message = "Actualización exitosa!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> DeleteDetPrd(DetPrdBloqueViewModel model)
        {
            var dto = new DetPrdBloqueDTO { Id = model.Id };
            await _prdBloquesService.DeleteDetPrd(dto);
            return Json(new { success = true, message = "Se eliminó registro!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditSubDetPrd(SubDetPrdBloqueViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new SubDetPrdBloqueDTO
            {
                Id = (int)model.subDetId,
                DetPrdBloquesId = model.DetPrdBloquesId,
                IdArticulo = model.IdArticulo,
                No = model.No,
                Hora = model.Hora,
                Silo = model.Silo,
                IdDensidad = model.IdDensidad,
                IdTipoBloque = model.IdTipoBloque,
                Peso = model.Peso,
                IdTipoFabricacion = model.IdTipoFabricacion,
                NumeroPedido = model.NumeroPedido,
                CodigoBloque = model.CodigoBloque,
                Observaciones = model.Observaciones,
                IdUsuarioActualizacion = userId
            };

            await _prdBloquesService.UpdateSubDetPrd(dto);
            return Json(new { success = true, message = "Actualización exitosa!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> DeleteSubDetPrd(SubDetPrdBloqueViewModel model)
        {
            var dto = new SubDetPrdBloqueDTO { Id = model.Id };
            await _prdBloquesService.DeleteSubDetPrd(dto);
            return Json(new { success = true, message = "Se eliminó registro!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> ValidarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdBloquesService.ValidatePrdBloqueByIdAsync(id, userId);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> AprobarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdBloquesService.AprovePrdBloqueByIdAsync(id, userId);
            return Json(result);
        }

        public async Task<ActionResult> GetDataReport(DateTime start, DateTime end)
        {
            var data = await _prdBloquesService.GetAllPrdBloqueWithDetailsAsync(start, end);
            return View(data);
        }

        private PrdBloqueViewModel MapToViewModel(PrdBloqueDTO modelDto, CrearPrdBloqueDto dtoCat, IEnumerable<IdentityUser> operarios)
        {
            var maquinasDict = dtoCat.CatMaquina?.GroupBy(m => m.Id).ToDictionary(g => g.Key, g => g.First().Nombre);
            var bloquesDict = dtoCat.CatalogoBloques?.GroupBy(c => c.Id).ToDictionary(g => g.Key, g => g.First().Bloque);
            var densidadDict = dtoCat.CatalogoDensidad?.GroupBy(d => d.Id).ToDictionary(g => g.Key, g => g.First().Descripcion);
            var tipoBloqueDict = dtoCat.CatalogoTipoBloque?.GroupBy(t => t.Id).ToDictionary(g => g.Key, g => g.First().Descripcion);
            var fabricacionDict = dtoCat.CatTipoFabricacion?.GroupBy(f => f.Id).ToDictionary(g => g.Key, g => g.First().Descripcion);

            return new PrdBloqueViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                Fecha = modelDto.Fecha,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                IdAprobadoSupervisor = modelDto.IdAprobadoSupervisor,
                IdAprobadoGerencia = modelDto.IdAprobadoGerencia,
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                CatalogoBloques = dtoCat.CatalogoBloques?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Bloque }),
                CatalogoDensidad = dtoCat.CatalogoDensidad?.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Descripcion }),
                CatalogoTipoBloque = dtoCat.CatalogoTipoBloque?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion }),
                TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion }),
                DetPrdBloques = modelDto.DetPrdBloques?.Select(d => new DetPrdBloqueViewModel
                {
                    Id = d.Id,
                    PrdBloqueId = d.PrdBloqueId,
                    IdMaquina = d.IdMaquina,
                    Maquina = maquinasDict?.GetValueOrDefault(d.IdMaquina),
                    IdCatBloque = d.IdCatBloque,
                    CatBloque = bloquesDict?.GetValueOrDefault(d.IdCatBloque),
                    ProduccionDia = d.ProduccionDia,
                    SubDetPrdBloques = d.SubDetPrdBloques?.Select(s => new SubDetPrdBloqueViewModel
                    {
                        Id = s.Id,
                        DetPrdBloquesId = s.DetPrdBloquesId,
                        IdArticulo = s.IdArticulo,
                        Articulo = bloquesDict?.GetValueOrDefault(s.IdArticulo),
                        No = s.No,
                        Hora = s.Hora,
                        Silo = s.Silo,
                        IdDensidad = s.IdDensidad,
                        Densidad = densidadDict?.GetValueOrDefault(s.IdDensidad),
                        IdTipoBloque = s.IdTipoBloque,
                        TipoBloque = tipoBloqueDict?.GetValueOrDefault(s.IdTipoBloque),
                        Peso = s.Peso,
                        IdTipoFabricacion = s.IdTipoFabricacion,
                        TipoFabricacion = fabricacionDict?.GetValueOrDefault(s.IdTipoFabricacion),
                        NumeroPedido = s.NumeroPedido,
                        CodigoBloque = s.CodigoBloque,
                        Observaciones = s.Observaciones
                    }).ToList()
                }).ToList()
            };
        }
    }
}
