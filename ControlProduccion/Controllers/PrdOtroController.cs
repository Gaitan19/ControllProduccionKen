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
    public class PrdOtroController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPrdOtroService _prdOtroService;

        public PrdOtroController(UserManager<IdentityUser> userManager, IPrdOtroService prdOtroService)
        {
               _prdOtroService = prdOtroService;
            _userManager = userManager;
        }
        // GET: PrdOtroController
        public async Task<ActionResult> Index()
        {
            var model =await _prdOtroService.GetAllAsync();
            return View(model);
        }

        // GET: PrdOtroController/Details/5
        public async Task<ActionResult> Details(int id)
        {

            // Catálogos y usuarios
            var dtoCat = await _prdOtroService.GetCreateData();
            var operarios = await _userManager
                .GetUsersInRoleAsync("Operario");

            // Datos de la entidad
            var modelDto = await _prdOtroService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var vm= new PrdOtroViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                Fecha = modelDto.Fecha,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                DetPrdOtros = modelDto.DetPrdOtros?.Select(x => new DetPrdOtroViewModel
                {
                    Id = x.Id,
                    PrdOtroId = x.PrdOtroId,
                    Actividad = x.Actividad,
                    DescripcionProducto = x.DescripcionProducto,
                    IdTipoFabricacion = x.IdTipoFabricacion,
                    TipoFabricacion = dtoCat.CatTipoFabricacion
                                       .FirstOrDefault(t => t.Id == x.IdTipoFabricacion)?
                                       .Descripcion,
                    NumeroPedido = x.NumeroPedido,
                    Nota = x.Nota,
                    Merma = x.Merma,
                    Comentario = x.Comentario,
                    Cantidad = x.Cantidad,
                    UnidadMedida = x.UnidadMedida,
                    HoraInicio = x.HoraInicio,
                    HoraFin = x.HoraFin
                }).ToList(),
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                TiposFabricacion=dtoCat.CatTipoFabricacion.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
            };


            return View(vm);
        }

        // GET: PrdOtroController/Create
        public async Task<ActionResult> Create()
        {
            PrdOtroViewModel model= new PrdOtroViewModel();
            // Populate dropdown lists asynchronously
            await PopulateDropdownListsAsync(model);
            return View(model);
        }

        // POST: PrdOtroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromBody] PrdOtroViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _userManager.GetUserId(User);
            model.IdUsuarioCreacion = userId;
            

            var detallePrd = model.DetPrdOtros?.Select(x => new DetPrdOtroDTO
            {
                Actividad = x.Actividad,
                DescripcionProducto = x.DescripcionProducto,
                IdTipoFabricacion = x.IdTipoFabricacion,
                NumeroPedido = x.NumeroPedido,
                Nota = x.Nota,
                Merma = x.Merma,
                Comentario = x.Comentario,
                Cantidad = x.Cantidad,
                UnidadMedida = x.UnidadMedida,
                HoraInicio = x.HoraInicio,
                HoraFin = x.HoraFin,
                IdUsuarioCreacion = userId
            }).ToList();


            var dto = new PrdOtroDto
            {
                IdUsuarios = model.IdUsuarios,
                Fecha = model.Fecha,
                TiempoParo = model.TiempoParo,
                IdUsuarioCreacion = model.IdUsuarioCreacion!,
                DetPrdOtros = detallePrd
              
            };

            await _prdOtroService.CreateAsync(dto);
            return Json(new { success = true, message = "Producción guardada!" });

        }

        // GET: PrdOtroController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var dtoCat= await _prdOtroService.GetCreateData();
            var userId = _userManager.GetUserId(User);
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");


            var modelDto = await _prdOtroService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

           var model = new PrdOtroViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                Fecha = modelDto.Fecha,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                DetPrdOtros = modelDto.DetPrdOtros?.Select(x => new DetPrdOtroViewModel
                {
                    Id = x.Id,
                    PrdOtroId = x.PrdOtroId,
                    Actividad = x.Actividad,
                    DescripcionProducto = x.DescripcionProducto,
                    IdTipoFabricacion = x.IdTipoFabricacion,
                    TipoFabricacion = dtoCat.CatTipoFabricacion
                                       .FirstOrDefault(t => t.Id == x.IdTipoFabricacion)?
                                       .Descripcion,
                    NumeroPedido = x.NumeroPedido,
                    Nota = x.Nota,
                    Merma = x.Merma,
                    Comentario = x.Comentario,
                    Cantidad = x.Cantidad,
                    UnidadMedida = x.UnidadMedida,
                    HoraInicio = x.HoraInicio,
                    HoraFin = x.HoraFin
                }).ToList(),
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                TiposFabricacion=dtoCat.CatTipoFabricacion.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
            };

            return View(model);
        }

        // POST: PrdOtroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> Edit(PrdOtroViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            if (!ModelState.IsValid)
            {
                var dtoCat = await _prdOtroService.GetCreateData();
               
                var operarios = await _userManager.GetUsersInRoleAsync("Operario");


                var modelDto = await _prdOtroService.GetByIdAsync(model.Id);


                var vm = new PrdOtroViewModel
                {
                    Id = modelDto.Id,
                    IdUsuarios = modelDto.IdUsuarios,
                    Fecha = modelDto.Fecha,
                    AprobadoSupervisor = modelDto.AprobadoSupervisor,
                    AprobadoGerencia = modelDto.AprobadoGerencia,
                    DetPrdOtros = modelDto.DetPrdOtros?.Select(x => new DetPrdOtroViewModel
                    {
                        Id = x.Id,
                        PrdOtroId = x.PrdOtroId,
                        Actividad = x.Actividad,
                        DescripcionProducto = x.DescripcionProducto,
                        IdTipoFabricacion = x.IdTipoFabricacion,
                        TipoFabricacion = dtoCat.CatTipoFabricacion
                                           .FirstOrDefault(t => t.Id == x.IdTipoFabricacion)?
                                           .Descripcion,
                        NumeroPedido = x.NumeroPedido,
                        Nota = x.Nota,
                        Merma = x.Merma,
                        Comentario = x.Comentario,
                        HoraInicio = x.HoraInicio,
                        HoraFin = x.HoraFin,
                        Cantidad = x.Cantidad,
                        UnidadMedida = x.UnidadMedida,
                    }).ToList(),
                    Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                    TiposFabricacion = dtoCat.CatTipoFabricacion.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
                };

                return View(vm);



            }

            try
            {
                

                var dto = new PrdOtroDto
                {
                    Id = model.Id,
                    IdUsuarios = model.IdUsuarios,
                    Fecha = model.Fecha,
                    TiempoParo = model.TiempoParo,
                    IdUsuarioActualizacion = userId,
                    FechaActualizacion = DateTime.Now
                };
                // Llamar al servicio para actualizar
                await _prdOtroService.UpdateAsync(dto);
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
                // _logger.LogError(ex, "Error updating PrdOtro");
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro. Intente nuevamente.");

                // Repopulate dropdowns and return the view
                var dtoCat = await _prdOtroService.GetCreateData();
              
                var operarios = await _userManager.GetUsersInRoleAsync("Operario");


                var modelDto = await _prdOtroService.GetByIdAsync(model.Id);


                var vm = new PrdOtroViewModel
                {
                    Id = modelDto.Id,
                    IdUsuarios = modelDto.IdUsuarios,
                    Fecha = modelDto.Fecha,
                    AprobadoSupervisor = modelDto.AprobadoSupervisor,
                    AprobadoGerencia = modelDto.AprobadoGerencia,
                    DetPrdOtros = modelDto.DetPrdOtros?.Select(x => new DetPrdOtroViewModel
                    {
                        Id = x.Id,
                        PrdOtroId = x.PrdOtroId,
                        Actividad = x.Actividad,
                        DescripcionProducto = x.DescripcionProducto,
                        IdTipoFabricacion = x.IdTipoFabricacion,
                        TipoFabricacion = dtoCat.CatTipoFabricacion
                                           .FirstOrDefault(t => t.Id == x.IdTipoFabricacion)?
                                           .Descripcion,
                        NumeroPedido = x.NumeroPedido,
                        Nota = x.Nota,
                        Merma = x.Merma,
                        Comentario = x.Comentario,
                        HoraInicio = x.HoraInicio,
                        HoraFin = x.HoraFin,
                        Cantidad = x.Cantidad,
                        UnidadMedida = x.UnidadMedida,
                    }).ToList(),
                    Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                    TiposFabricacion = dtoCat.CatTipoFabricacion.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
                };

                return View(vm);

            }

        }

        // GET: PrdOtroController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrdOtroController/Delete/5
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
        private async Task PopulateDropdownListsAsync(PrdOtroViewModel model)
        {
            var dtoCat = await _prdOtroService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");
            model.Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName });
            model.TiposFabricacion=dtoCat.CatTipoFabricacion.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion });

        }
        //
        [HttpPost]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> ValidarPrd(int id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var result = await _prdOtroService.ValidatePrdOtroByIdAsync(id, userId);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> AprobarPrd(int id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var result = await _prdOtroService.AprovePrdOtroByIdAsync(id, userId);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(false);
            }
        }


        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrd(DetPrdOtroViewModel model)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                
                // Validate that detrpdId has a value
                if (!model.detrpdId.HasValue || model.detrpdId.Value <= 0)
                {
                    return Json(new { success = false, message = "ID de detalle inválido" });
                }

                var dto = new DetPrdOtroDTO
                {
                    Id = model.detrpdId.Value,
                    Actividad = model.Actividad,
                    DescripcionProducto = model.DescripcionProducto,
                    IdTipoFabricacion = model.IdTipoFabricacion,
                    NumeroPedido = model.NumeroPedido,
                    Nota = model.Nota,
                    Merma = model.Merma,
                    Comentario = model.Comentario,
                    Cantidad = model.Cantidad,
                    UnidadMedida = model.UnidadMedida,
                    HoraInicio = model.HoraInicio,
                    HoraFin = model.HoraFin,
                    IdUsuarioActualizacion = userId
                };

                await _prdOtroService.UpdateDetPrd(dto);

                return Json(new { success = true, message = "Actualización exitosa!" });
            }
            catch (Exception ex)
            {
                // Log the exception if logging is available
                return Json(new { success = false, message = $"Error al actualizar: {ex.Message}" });
            }
        }

        public async Task<ActionResult> GetDataReport(DateTime start, DateTime end)
        {
            // Validate and ensure DateTime parameters are within valid SQL Server range
            start = ValidateDateTimeParameter(start);
            end = ValidateDateTimeParameter(end);
            
            var data = await _prdOtroService.GetAllPrdOtroWithDetailsAsync(start, end);
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