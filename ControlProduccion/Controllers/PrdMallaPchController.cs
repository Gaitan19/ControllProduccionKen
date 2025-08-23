using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using ControlProduccion.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlProduccion.Controllers
{
    [Authorize]
    public class PrdMallaPchController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPrdMallaPchService _prdMallaPchService;

        public PrdMallaPchController(
            UserManager<IdentityUser> userManager,
            IPrdMallaPchService prdMallaPchService)
        {
            _userManager = userManager;
            _prdMallaPchService = prdMallaPchService;
        }

        // GET: PrdMallaPchController
        public async Task<ActionResult> Index()
        {
            var model = await _prdMallaPchService.GetAllAsync();
            return View(model);
        }

        // GET: PrdMallaPchController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var dtoCat = await _prdMallaPchService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var model = await _prdMallaPchService.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var vm = MapToViewModel(model, dtoCat, operarios);
            return View(vm);
        }

        // GET: PrdMallaPchController/Create
        public async Task<ActionResult> Create()
        {
            var dtoCat = await _prdMallaPchService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var vm = new PrdMallaPchViewModel
            {
                Operarios = operarios.Where(u => !u.LockoutEnd.HasValue || u.LockoutEnd.Value <= DateTimeOffset.Now)
                    .Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.UserName
                    }),
                Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nombre
                }),
                TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Descripcion
                }),
                TiposMalla = dtoCat.CatTipoMalla?.Select(tm => new SelectListItem
                {
                    Value = tm.Id.ToString(),
                    Text = tm.Cuadricula
                })
            };

            return View(vm);
        }

        // POST: PrdMallaPchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] PrdMallaPchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            var detPrdPchMaquinaAs = model.DetPrdPchMaquinaAs?.Select(x => new DetPrdPchMaquinaADTO
            {
                IdMaquina = x.IdMaquina,
                HilosTransversalesUn = x.HilosTransversalesUn,
                MermaHilosTransversalesKg = x.MermaHilosTransversalesKg,
                IdTipoFabricacion = x.IdTipoFabricacion,
                NumeroPedido = x.NumeroPedido,
                Longitud = x.Longitud,
                Cantidad = x.Cantidad,
                NumeroAlambreA = x.NumeroAlambreA,
                PesoAlambreKgA = x.PesoAlambreKgA,
                IdUsuarioCreacion = userId,
                FechaCreacion = DateTime.Now
            }).ToList() ?? new List<DetPrdPchMaquinaADTO>();

            var detPrdPchMaquinaBs = model.DetPrdPchMaquinaBs?.Select(x => new DetPrdPchMaquinaBDTO
            {
                IdMaquina = x.IdMaquina,
                HilosLongitudinalesUn = x.HilosLongitudinalesUn,
                MermaHilosLongitudinalesKg = x.MermaHilosLongitudinalesKg,
                IdTipoFabricacion = x.IdTipoFabricacion,
                NumeroPedido = x.NumeroPedido,
                Longitud = x.Longitud,
                Cantidad = x.Cantidad,
                NumeroAlambreB = x.NumeroAlambreB,
                PesoAlambreKgB = x.PesoAlambreKgB,
                IdUsuarioCreacion = userId,
                FechaCreacion = DateTime.Now
            }).ToList() ?? new List<DetPrdPchMaquinaBDTO>();

            var detPrdPchMaquinaCs = model.DetPrdPchMaquinaCs?.Select(x => new DetPrdPchMaquinaCDTO
            {
                IdMaquina = x.IdMaquina,
                IdTipoMalla = x.IdTipoMalla,
                MermaMallasKg = x.MermaMallasKg,
                IdTipoFabricacion = x.IdTipoFabricacion,
                NumeroPedido = x.NumeroPedido,
                Longitud = x.Longitud,
                Cantidad = x.Cantidad,
                IdUsuarioCreacion = userId,
                FechaCreacion = DateTime.Now
            }).ToList() ?? new List<DetPrdPchMaquinaCDTO>();

            var mallaPchDTO = new PrdMallaPchDTO
            {
                IdUsuarios = model.IdUsuarios,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                IdUsuarioCreacion = userId,
                FechaCreacion = DateTime.Now,
                DetPrdPchMaquinaAs = detPrdPchMaquinaAs,
                DetPrdPchMaquinaBs = detPrdPchMaquinaBs,
                DetPrdPchMaquinaCs = detPrdPchMaquinaCs
            };

            await _prdMallaPchService.CreateAsync(mallaPchDTO);

            return Json(new { success = true, message = "Producción guardada!" });
        }

        // GET: PrdMallaPchController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var dtoCat = await _prdMallaPchService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var model = await _prdMallaPchService.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var vm = MapToViewModel(model, dtoCat, operarios);
            return View(vm);
        }

        // POST: PrdMallaPchController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> Edit(PrdMallaPchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var userId = _userManager.GetUserId(User);

            var dto = new PrdMallaPchDTO
            {
                Id = model.Id,
                IdUsuarios = model.IdUsuarios,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                IdUsuarioActualizacion = userId,
                FechaActualizacion = DateTime.Now
            };

            await _prdMallaPchService.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        // POST: PrdMallaPchController/EditDetPrd for Maquina A
        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrdMaquinaA(DetPrdPchMaquinaAViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetPrdPchMaquinaADTO
            {
                Id = (int)model.DetPrdPchMaquinaAId,
                IdMaquina = model.IdMaquina,
                HilosTransversalesUn = model.HilosTransversalesUn,
                MermaHilosTransversalesKg = model.MermaHilosTransversalesKg,
                IdTipoFabricacion = model.IdTipoFabricacion,
                NumeroPedido = model.NumeroPedido,
                Longitud = model.Longitud,
                Cantidad = model.Cantidad,
                NumeroAlambreA = model.NumeroAlambreA,
                PesoAlambreKgA = model.PesoAlambreKgA,
                IdUsuarioActualizacion = userId
            };

            await _prdMallaPchService.UpdateDetPrd(dto);

            return Json(new { success = true, message = "Actualización exitosa!" });
        }

        // POST: PrdMallaPchController/EditDetPrd for Maquina B
        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrdMaquinaB(DetPrdPchMaquinaBViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetPrdPchMaquinaBDTO
            {
                Id = (int)model.DetPrdPchMaquinaBId,
                IdMaquina = model.IdMaquina,
                HilosLongitudinalesUn = model.HilosLongitudinalesUn,
                MermaHilosLongitudinalesKg = model.MermaHilosLongitudinalesKg,
                IdTipoFabricacion = model.IdTipoFabricacion,
                NumeroPedido = model.NumeroPedido,
                Longitud = model.Longitud,
                Cantidad = model.Cantidad,
                NumeroAlambreB = model.NumeroAlambreB,
                PesoAlambreKgB = model.PesoAlambreKgB,
                IdUsuarioActualizacion = userId
            };

            await _prdMallaPchService.UpdateDetPrd(dto);

            return Json(new { success = true, message = "Actualización exitosa!" });
        }

        // POST: PrdMallaPchController/EditDetPrd for Maquina C
        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrdMaquinaC(DetPrdPchMaquinaCViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetPrdPchMaquinaCDTO
            {
                Id = (int)model.DetPrdPchMaquinaCId,
                IdMaquina = model.IdMaquina,
                IdTipoMalla = model.IdTipoMalla,
                MermaMallasKg = model.MermaMallasKg,
                IdTipoFabricacion = model.IdTipoFabricacion,
                NumeroPedido = model.NumeroPedido,
                Longitud = model.Longitud,
                Cantidad = model.Cantidad,
                IdUsuarioActualizacion = userId
            };

            await _prdMallaPchService.UpdateDetPrd(dto);

            return Json(new { success = true, message = "Actualización exitosa!" });
        }

   

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> ValidarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdMallaPchService.ValidatePrdMallaPchByIdAsync(id, userId);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> AprobarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdMallaPchService.AprovePrdMallaPchByIdAsync(id, userId);
            return Json(result);
        }

        //public async Task<ActionResult> GetDataReport(DateTime start, DateTime end)
        //{
        //    // Validate and ensure DateTime parameters are within valid SQL Server range
        //    start = ValidateDateTimeParameter(start);
        //    end = ValidateDateTimeParameter(end);

        //    var data = await _prdMallasCovintecService
        //        .GetAllMallasProduccionWithDetailsAsync(start, end);
        //    return View(data);
        //}
        public async Task<ActionResult> GetDataReport(DateTime start, DateTime end)
        {
            // Validate and ensure DateTime parameters are within valid SQL Server range
            start = ValidateDateTimeParameter(start);
            end = ValidateDateTimeParameter(end);
            var data = await _prdMallaPchService
                .GetAllPrdMallaPCHWithDetailsAsync(start, end);
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

        private PrdMallaPchViewModel MapToViewModel(PrdMallaPchDTO modelDto, CrearPrdMallaPchDTO dtoCat, IEnumerable<IdentityUser> operarios)
        {
            // Create dictionaries for performance
            var maquinasDict = dtoCat.CatMaquina?.GroupBy(m => m.Id).ToDictionary(g => g.Key, g => g.First().Nombre);
            var tiposFabDict = dtoCat.CatTipoFabricacion?.GroupBy(t => t.Id).ToDictionary(g => g.Key, g => g.First().Descripcion);
            var tiposMallaDict = dtoCat.CatTipoMalla?.GroupBy(tm => tm.Id).ToDictionary(g => g.Key, g => g.First().Cuadricula);

            return new PrdMallaPchViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                Fecha = modelDto.Fecha,
                Observaciones = modelDto.Observaciones,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                IdAprobadoSupervisor = modelDto.IdAprobadoSupervisor,
                IdAprobadoGerencia = modelDto.IdAprobadoGerencia,
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion }),
                TiposMalla = dtoCat.CatTipoMalla?.Select(tm => new SelectListItem { Value = tm.Id.ToString(), Text = tm.Cuadricula }),
                ProduccionTotal = modelDto.ProduccionDia,
                DetPrdPchMaquinaAs = modelDto.DetPrdPchMaquinaAs?.Select(d => new DetPrdPchMaquinaAViewModel
                {
                    Id = d.Id,
                    DetPrdPchMaquinaAId = d.Id,
                    PrdMallaPchId = d.PrdMallaPchId,
                    IdMaquina = d.IdMaquina,
                    HilosTransversalesUn = d.HilosTransversalesUn,
                    MermaHilosTransversalesKg = d.MermaHilosTransversalesKg,
                    IdTipoFabricacion = d.IdTipoFabricacion,
                    NumeroPedido = d.NumeroPedido,
                    Longitud = d.Longitud,
                    Cantidad = d.Cantidad,
                    Produccion = d.Produccion,
                    NumeroAlambreA = d.NumeroAlambreA,
                    PesoAlambreKgA = d.PesoAlambreKgA,
                    Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre })
                }).ToList(),
                DetPrdPchMaquinaBs = modelDto.DetPrdPchMaquinaBs?.Select(d => new DetPrdPchMaquinaBViewModel
                {
                    Id = d.Id,
                    DetPrdPchMaquinaBId = d.Id,
                    PrdMallaPchId = d.PrdMallaPchId,
                    IdMaquina = d.IdMaquina,
                    HilosLongitudinalesUn = d.HilosLongitudinalesUn,
                    MermaHilosLongitudinalesKg = d.MermaHilosLongitudinalesKg,
                    IdTipoFabricacion = d.IdTipoFabricacion,
                    NumeroPedido = d.NumeroPedido,
                    Longitud = d.Longitud,
                    Cantidad = d.Cantidad,
                    Produccion = d.Produccion,
                    NumeroAlambreB = d.NumeroAlambreB,
                    PesoAlambreKgB = d.PesoAlambreKgB,
                    Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre })
                }).ToList(),
                DetPrdPchMaquinaCs = modelDto.DetPrdPchMaquinaCs?.Select(d => new DetPrdPchMaquinaCViewModel
                {
                    Id = d.Id,
                    DetPrdPchMaquinaCId = d.Id,
                    PrdMallaPchId = d.PrdMallaPchId,
                    IdMaquina = d.IdMaquina,
                    IdTipoMalla = d.IdTipoMalla,
                    MermaMallasKg = d.MermaMallasKg,
                    IdTipoFabricacion = d.IdTipoFabricacion,
                    NumeroPedido = d.NumeroPedido,
                    Longitud = d.Longitud,
                    Cantidad = d.Cantidad,
                    Produccion = d.Produccion,
                    Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre })
                }).ToList()
            };
        }
    }
}
