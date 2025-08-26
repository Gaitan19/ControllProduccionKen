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
    public class PrdPreExpansionController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPrdPreExpansionService _prdPreExpansionService;

       public PrdPreExpansionController(
            UserManager<IdentityUser> userManager,
            IPrdPreExpansionService prdPreExpansionService)
        {
            _userManager = userManager;
            _prdPreExpansionService = prdPreExpansionService;
        }

        // GET: PrdPreExpansionController
        public async Task<ActionResult> Index()
        {
            var model = await _prdPreExpansionService.GetAllAsync();
            return View(model);
        }

        // GET: PrdPreExpansionController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            // Catálogos y usuarios
            var dtoCat = await _prdPreExpansionService.GetCatalogosAsync();
            var operarios = await _userManager
                .GetUsersInRoleAsync("Operario");

            // Datos de la entidad
            var modelDto = await _prdPreExpansionService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var vm = new PrdPreExpansionViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                IdMaquina = modelDto.IdMaquina,
                Fecha = modelDto.Fecha,
                HoraInicio = modelDto.HoraInicio,
                HoraFin = modelDto.HoraFin,
                PresionCaldera = modelDto.PresionCaldera,
                Lote = modelDto.Lote,
                FechaProduccion = modelDto.FechaProduccion,
                CodigoSaco = modelDto.CodigoSaco,
                IdTipoFabricacion = modelDto.IdTipoFabricacion,
                NumeroPedido = modelDto.NumeroPedido,
                Observaciones = modelDto.Observaciones,
                TiempoParo = modelDto.TiempoParo,
                IdTipoReporte = modelDto.IdTipoReporte,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                NotaSupervisor = modelDto.NotaSupervisor,
                DetPrdPreExpansions = modelDto.DetPrdpreExpansions?.Select(x => new DetPrdPreExpansionViewModel
                {
                    Id = x.Id,
                    PrdpreExpansionId = x.PrdpreExpansionId,
                    Hora = x.Hora,
                    NoBatch = x.NoBatch,
                    DensidadEsperada = x.DensidadEsperada,
                    PesoBatchGr = x.PesoBatchGr,
                    Densidad = x.Densidad,
                    KgPorBatch = x.KgPorBatch,
                    Paso = x.Paso,
                    PresionPsi = x.PresionPsi,
                    TiempoBatchSeg = x.TiempoBatchSeg,
                    TemperaturaC = x.TemperaturaC,
                    Silo = x.Silo
                }).ToList(),
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
            };

            return View(vm);
        }

        // GET: PrdPreExpansionController/Create
        public async Task<ActionResult> Create()
        {
            PrdPreExpansionViewModel model = new PrdPreExpansionViewModel();
            // Populate dropdown lists asynchronously
            await PopulateDropdownListsAsync(model);
            return View(model);
        }

        // POST: PrdPreExpansionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromBody] PrdPreExpansionViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = _userManager.GetUserId(User);
                model.IdUsuarioCreacion = userId;

                var detallePrd = model.DetPrdPreExpansions?.Select(x => new DetPrdpreExpansionDTO
                {
                    Hora = x.Hora,
                    NoBatch = x.NoBatch,
                    DensidadEsperada = x.DensidadEsperada,
                    PesoBatchGr = x.PesoBatchGr,
                    Densidad = x.Densidad,
                    KgPorBatch = x.KgPorBatch,
                    PresionPsi = x.PresionPsi,
                    TiempoBatchSeg = x.TiempoBatchSeg,
                    TemperaturaC = x.TemperaturaC,
                    Silo = x.Silo,
                    Paso = x.Paso,
                    IdUsuarioCreacion = userId
                }).ToList();

                var dto = new PrdpreExpansionDto
                {
                    IdUsuarios = model.IdUsuarios,
                    IdMaquina = model.IdMaquina,
                    Fecha = model.Fecha,
                    HoraInicio = model.HoraInicio,
                    HoraFin = model.HoraFin,
                    PresionCaldera = model.PresionCaldera,
                    Lote = model.Lote,
                    FechaProduccion = model.FechaProduccion,
                    CodigoSaco = model.CodigoSaco,
                    IdTipoFabricacion = model.IdTipoFabricacion,
                    NumeroPedido = model.NumeroPedido,
                    Observaciones = model.Observaciones,
                    TiempoParo = model.TiempoParo,
                    IdTipoReporte = model.IdTipoReporte,
                    IdUsuarioCreacion = model.IdUsuarioCreacion!,
                    DetPrdpreExpansions = detallePrd
                };

                await _prdPreExpansionService.CreateAsync(dto);
                return Json(new { success = true, message = "Producción guardada!" });
            }
            catch (Exception ex)
            {
                // Log the error (you can add proper logging here)
                return Json(new { success = false, message = "Error al guardar la producción: " + ex.Message });
            }
        }

        // GET: PrdPreExpansionController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var dtoCat = await _prdPreExpansionService.GetCatalogosAsync();
            var userId = _userManager.GetUserId(User);
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var modelDto = await _prdPreExpansionService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var model = new PrdPreExpansionViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                IdMaquina = modelDto.IdMaquina,
                Fecha = modelDto.Fecha,
                HoraInicio = modelDto.HoraInicio,
                HoraFin = modelDto.HoraFin,
                PresionCaldera = modelDto.PresionCaldera,
                Lote = modelDto.Lote,
                FechaProduccion = modelDto.FechaProduccion,
                CodigoSaco = modelDto.CodigoSaco,
                IdTipoFabricacion = modelDto.IdTipoFabricacion,
                NumeroPedido = modelDto.NumeroPedido,
                Observaciones = modelDto.Observaciones,
                TiempoParo = modelDto.TiempoParo,
                IdTipoReporte = modelDto.IdTipoReporte,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                NotaSupervisor = modelDto.NotaSupervisor,
                DetPrdPreExpansions = modelDto.DetPrdpreExpansions?.Select(x => new DetPrdPreExpansionViewModel
                {
                    Id = x.Id,
                    PrdpreExpansionId = x.PrdpreExpansionId,
                    Hora = x.Hora,
                    NoBatch = x.NoBatch,
                    DensidadEsperada = x.DensidadEsperada,
                    PesoBatchGr = x.PesoBatchGr,
                    Densidad = x.Densidad,
                    KgPorBatch = x.KgPorBatch,
                    PresionPsi = x.PresionPsi,
                    TiempoBatchSeg = x.TiempoBatchSeg,
                    TemperaturaC = x.TemperaturaC,
                    Silo = x.Silo,
                    Paso = x.Paso
                }).ToList(),
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
            };

            return View(model);
        }

        // POST: PrdPreExpansionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> Edit(PrdPreExpansionViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            if (!ModelState.IsValid)
            {
                var dtoCat = await _prdPreExpansionService.GetCatalogosAsync();
                var operarios = await _userManager.GetUsersInRoleAsync("Operario");

                var modelDto = await _prdPreExpansionService.GetByIdAsync(model.Id);

                var vm = new PrdPreExpansionViewModel
                {
                    Id = modelDto.Id,
                    IdUsuarios = modelDto.IdUsuarios,
                    IdMaquina = modelDto.IdMaquina,
                    Fecha = modelDto.Fecha,
                    HoraInicio = modelDto.HoraInicio,
                    HoraFin = modelDto.HoraFin,
                    PresionCaldera = modelDto.PresionCaldera,
                    Lote = modelDto.Lote,
                    FechaProduccion = modelDto.FechaProduccion,
                    CodigoSaco = modelDto.CodigoSaco,
                    IdTipoFabricacion = modelDto.IdTipoFabricacion,
                    NumeroPedido = modelDto.NumeroPedido,
                    Observaciones = modelDto.Observaciones,
                    TiempoParo = modelDto.TiempoParo,
                    IdTipoReporte = modelDto.IdTipoReporte,
                    AprobadoSupervisor = modelDto.AprobadoSupervisor,
                    AprobadoGerencia = modelDto.AprobadoGerencia,
                    DetPrdPreExpansions = modelDto.DetPrdpreExpansions?.Select(x => new DetPrdPreExpansionViewModel
                    {
                        Id = x.Id,
                        PrdpreExpansionId = x.PrdpreExpansionId,
                        Hora = x.Hora,
                        NoBatch = x.NoBatch,
                        DensidadEsperada = x.DensidadEsperada,
                        PesoBatchGr = x.PesoBatchGr,
                        Densidad = x.Densidad,
                        KgPorBatch = x.KgPorBatch,
                        PresionPsi = x.PresionPsi,
                        TiempoBatchSeg = x.TiempoBatchSeg,
                        TemperaturaC = x.TemperaturaC,
                        Silo = x.Silo
                    }).ToList(),
                    Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                    Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                    TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
                };

                return View(vm);
            }

            try
            {
                var dto = new PrdpreExpansionDto
                {
                    Id = model.Id,
                    IdUsuarios = model.IdUsuarios,
                    IdMaquina = model.IdMaquina,
                    Fecha = model.Fecha,
                    HoraInicio = model.HoraInicio,
                    HoraFin = model.HoraFin,
                    PresionCaldera = model.PresionCaldera,
                    Lote = model.Lote,
                    FechaProduccion = model.FechaProduccion,
                    CodigoSaco = model.CodigoSaco,
                    IdTipoFabricacion = model.IdTipoFabricacion,
                    NumeroPedido = model.NumeroPedido,
                    Observaciones = model.Observaciones,
                    TiempoParo = model.TiempoParo,
                    IdTipoReporte = model.IdTipoReporte,
                    IdUsuarioActualizacion = userId,
                    FechaActualizacion = DateTime.Now
                };
                // Llamar al servicio para actualizar
                await _prdPreExpansionService.UpdateAsync(dto);
                // Redirigir a la lista de producciones

                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception here if logging is available
                // _logger.LogError(ex, "Error updating PrdPreExpansion");
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro. Intente nuevamente.");

                // Repopulate dropdowns and return the view
                var dtoCat = await _prdPreExpansionService.GetCatalogosAsync();
                var operarios = await _userManager.GetUsersInRoleAsync("Operario");

                var modelDto = await _prdPreExpansionService.GetByIdAsync(model.Id);

                var vm = new PrdPreExpansionViewModel
                {
                    Id = modelDto.Id,
                    IdUsuarios = modelDto.IdUsuarios,
                    IdMaquina = modelDto.IdMaquina,
                    Fecha = modelDto.Fecha,
                    HoraInicio = modelDto.HoraInicio,
                    HoraFin = modelDto.HoraFin,
                    PresionCaldera = modelDto.PresionCaldera,
                    Lote = modelDto.Lote,
                    FechaProduccion = modelDto.FechaProduccion,
                    CodigoSaco = modelDto.CodigoSaco,
                    IdTipoFabricacion = modelDto.IdTipoFabricacion,
                    NumeroPedido = modelDto.NumeroPedido,
                    Observaciones = modelDto.Observaciones,
                    TiempoParo = modelDto.TiempoParo,
                    IdTipoReporte = modelDto.IdTipoReporte,
                    AprobadoSupervisor = modelDto.AprobadoSupervisor,
                    AprobadoGerencia = modelDto.AprobadoGerencia,
                    DetPrdPreExpansions = modelDto.DetPrdpreExpansions?.Select(x => new DetPrdPreExpansionViewModel
                    {
                        Id = x.Id,
                        PrdpreExpansionId = x.PrdpreExpansionId,
                        Hora = x.Hora,
                        NoBatch = x.NoBatch,
                        DensidadEsperada = x.DensidadEsperada,
                        PesoBatchGr = x.PesoBatchGr,
                        Densidad = x.Densidad,
                        KgPorBatch = x.KgPorBatch,
                        PresionPsi = x.PresionPsi,
                        TiempoBatchSeg = x.TiempoBatchSeg,
                        TemperaturaC = x.TemperaturaC,
                        Silo = x.Silo
                    }).ToList(),
                    Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                    Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                    TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
                };

                return View(vm);
            }
        }

        // GET: PrdPreExpansionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrdPreExpansionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //metodos endpoints
        private async Task PopulateDropdownListsAsync(PrdPreExpansionViewModel model)
        {
            var dtoCat = await _prdPreExpansionService.GetCatalogosAsync();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");
            model.Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName });
            model.Maquinas = dtoCat.CatMaquina?.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre });
            model.TiposFabricacion = dtoCat.CatTipoFabricacion?.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion });
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> ValidarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdPreExpansionService.ApprobarSupervisorAsync(id, userId);

            return Json(result);
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> AprobarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdPreExpansionService.AprobarGerenciaAsync(id, userId);

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> SaveNotaSupervisor(int id, string notaSupervisor)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdPreExpansionService.UpdateNotaSupervisorAsync(id, notaSupervisor, userId);
            
            if (result)
            {
                return Json(new { success = true, message = "Nota guardada exitosamente" });
            }
            else
            {
                return Json(new { success = false, message = "Error al guardar la nota" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrd(DetPrdPreExpansionViewModel model)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var dto = new DetPrdpreExpansionDTO
                {
                    Id = (int)model.DetPrdPreExpansionId,
                    PrdpreExpansionId = model.PrdpreExpansionId,
                    Hora = model.Hora,
                    NoBatch = model.NoBatch,
                    DensidadEsperada = model.DensidadEsperada,
                    PesoBatchGr = model.PesoBatchGr,
                    Densidad = model.Densidad,
                    KgPorBatch = model.KgPorBatch,
                    PresionPsi = model.PresionPsi,
                    TiempoBatchSeg = model.TiempoBatchSeg,
                    TemperaturaC = model.TemperaturaC,
                    Silo = model.Silo,
                    IdUsuarioActualizacion = userId,
                    FechaActualizacion = DateTime.Now
                };

                await _prdPreExpansionService.UpdateDetPrd(dto);

                return Json(new { success = true, message = "Actualización exitosa!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al actualizar: " + ex.Message });
            }
        }

        public async Task<ActionResult> GetDataReport(DateTime? start, DateTime? end)
        {
            // Validate and ensure DateTime parameters are within valid SQL Server range
            var startDate = start.HasValue ? ValidateDateTimeParameter(start.Value) : DateTime.Today.AddDays(-1);
            var endDate = end.HasValue ? ValidateDateTimeParameter(end.Value) : DateTime.Today;

            var data = await _prdPreExpansionService.GetAllPrdPreExpansionWithDetailsAsync(startDate, endDate);
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
    }
}
