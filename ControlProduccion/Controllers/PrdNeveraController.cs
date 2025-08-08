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
    public class PrdNeveraController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPrdNeveraService _prdNeveraService;

        public PrdNeveraController(UserManager<IdentityUser> userManager, IPrdNeveraService prdNeveraService)
        {
               _prdNeveraService = prdNeveraService;
            _userManager = userManager;
        }
        // GET: PrdNeveraController
        public async Task<ActionResult> Index()
        {
            var model =await _prdNeveraService.GetAllAsync();
            return View(model);
        }

        // GET: PrdNeveraController/Details/5
        public async Task<ActionResult> Details(int id)
        {

            // Catálogos y usuarios
            var dtoCat = await _prdNeveraService.GetCreateData();
            var operarios = await _userManager
                .GetUsersInRoleAsync("Operario");

            // Datos de la entidad
            var modelDto = await _prdNeveraService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var vm= new PrdNeveraViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                IdMaquina = modelDto.IdMaquina,
                Fecha = modelDto.Fecha,
                Observaciones = modelDto.Observaciones,
                HoraInicio = modelDto.HoraInicio,
                HoraFin = modelDto.HoraFin,
                TiempoParo = modelDto.TiempoParo,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                DetPrdNeveras = modelDto.DetPrdNeveras?.Select(x => new DetPrdNeveraViewModel
                {
                    Posicion=x.Posicion,
                    IdArticulo = x.IdArticulo,
                    Articulo= dtoCat.CatalogoNeveras
                                  .FirstOrDefault(c => c.Id == x.IdArticulo)?
                                  .Descripcion,
                    CantidadConforme = x.CantidadConforme,
                    CantidadNoConforme = x.CantidadNoConforme,
                    IdTipoFabricacion = x.IdTipoFabricacion,
                    TipoFabricacion = dtoCat.CatTipoFabricacion
                                       .FirstOrDefault(t => t.Id == x.IdTipoFabricacion)?
                                       .Descripcion,
                    NumeroPedido = x.NumeroPedido
                }).ToList(),
                Articulos = dtoCat.CatalogoNeveras.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Codigo + "-" + a.Descripcion }),
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Maquinas = dtoCat.CatMaquina.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                TiposFabricacion=dtoCat.CatTipoFabricacion.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
            };


            return View(vm);
        }

        // GET: PrdNeveraController/Create
        public async Task<ActionResult> Create()
        {
            PrdNeveraViewModel model= new PrdNeveraViewModel();
            // Populate dropdown lists asynchronously
            await PopulateDropdownListsAsync(model);
            return View(model);
        }

        // POST: PrdNeveraController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromBody] PrdNeveraViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _userManager.GetUserId(User);
            model.IdUsuarioCreacion = userId;
            

            var detallePrd = model.DetPrdNeveras?.Select(x => new DetPrdNeveraDTO
            {
                IdArticulo = x.IdArticulo,
                CantidadConforme = x.CantidadConforme,
                CantidadNoConforme = x.CantidadNoConforme,
                IdTipoFabricacion = x.IdTipoFabricacion,
                NumeroPedido = x.NumeroPedido,
                IdUsuarioCreacion = userId
            }).ToList();


            var dto = new PrdNeveraDto
            {
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
               HoraInicio= model.HoraInicio,
               HoraFin = model.HoraFin,
                TiempoParo = model.TiempoParo,
                IdUsuarioCreacion = model.IdUsuarioCreacion!,
                DetPrdNeveras = detallePrd
              
            };

            await _prdNeveraService.CreateAsync(dto);
            return Json(new { success = true, message = "Producción guardada!" });

        }

        // GET: PrdNeveraController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var dtoCat= await _prdNeveraService.GetCreateData();
            var userId = _userManager.GetUserId(User);
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");


            var modelDto = await _prdNeveraService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

           var model = new PrdNeveraViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                IdMaquina = modelDto.IdMaquina,
                Fecha = modelDto.Fecha,
                Observaciones = modelDto.Observaciones,
                HoraInicio = modelDto.HoraInicio,
                HoraFin = modelDto.HoraFin,
                TiempoParo = modelDto.TiempoParo,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                DetPrdNeveras = modelDto.DetPrdNeveras?.Select(x => new DetPrdNeveraViewModel
                {
                    Id= x.Id,
                    PrdNeveraId = x.PrdNeveraId,
                    Posicion =x.Posicion,
                    IdArticulo = x.IdArticulo,
                    Articulo= dtoCat.CatalogoNeveras
                                  .FirstOrDefault(c => c.Id == x.IdArticulo)?
                                  .Descripcion,
                    CantidadConforme = x.CantidadConforme,
                    CantidadNoConforme = x.CantidadNoConforme,
                    IdTipoFabricacion = x.IdTipoFabricacion,
                    TipoFabricacion = dtoCat.CatTipoFabricacion
                                       .FirstOrDefault(t => t.Id == x.IdTipoFabricacion)?
                                       .Descripcion,
                    NumeroPedido = x.NumeroPedido
                }).ToList(),
                Articulos = dtoCat.CatalogoNeveras.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Codigo + "-" + a.Descripcion }),
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                Maquinas = dtoCat.CatMaquina.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                TiposFabricacion=dtoCat.CatTipoFabricacion.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
            };

            return View(model);
        }

        // POST: PrdNeveraController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> Edit(PrdNeveraViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            if (!ModelState.IsValid)
            {
                var dtoCat = await _prdNeveraService.GetCreateData();
               
                var operarios = await _userManager.GetUsersInRoleAsync("Operario");


                var modelDto = await _prdNeveraService.GetByIdAsync(model.Id);


                var vm = new PrdNeveraViewModel
                {
                    Id = modelDto.Id,
                    IdUsuarios = modelDto.IdUsuarios,
                    IdMaquina = modelDto.IdMaquina,
                    Fecha = modelDto.Fecha,
                    Observaciones = modelDto.Observaciones,
                    HoraInicio = modelDto.HoraInicio,
                    HoraFin = modelDto.HoraFin,
                    TiempoParo = modelDto.TiempoParo,
                    AprobadoSupervisor = modelDto.AprobadoSupervisor,
                    AprobadoGerencia = modelDto.AprobadoGerencia,
                    DetPrdNeveras = modelDto.DetPrdNeveras?.Select(x => new DetPrdNeveraViewModel
                    {
                        Posicion = x.Posicion,
                        IdArticulo = x.IdArticulo,
                        Articulo = dtoCat.CatalogoNeveras
                                      .FirstOrDefault(c => c.Id == x.IdArticulo)?
                                      .Descripcion,
                        CantidadConforme = x.CantidadConforme,
                        CantidadNoConforme = x.CantidadNoConforme,
                        IdTipoFabricacion = x.IdTipoFabricacion,
                        TipoFabricacion = dtoCat.CatTipoFabricacion
                                           .FirstOrDefault(t => t.Id == x.IdTipoFabricacion)?
                                           .Descripcion,
                        NumeroPedido = x.NumeroPedido
                    }).ToList(),
                    Articulos = dtoCat.CatalogoNeveras.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Codigo + "-" + a.Descripcion }),
                    Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                    Maquinas = dtoCat.CatMaquina.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                    TiposFabricacion = dtoCat.CatTipoFabricacion.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
                };

                return View(vm);



            }

            try
            {
                

                var dto = new PrdNeveraDto
                {
                    Id = model.Id,
                    IdUsuarios = model.IdUsuarios,
                    IdMaquina = model.IdMaquina,
                    Fecha = model.Fecha,
                    Observaciones = model.Observaciones,
                    HoraInicio = model.HoraInicio,
                    HoraFin = model.HoraFin,
                    TiempoParo = model.TiempoParo,
                    IdUsuarioActualizacion = userId,
                    FechaActualizacion = DateTime.Now
                };
                // Llamar al servicio para actualizar
                await _prdNeveraService.UpdateAsync(dto);
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
                // _logger.LogError(ex, "Error updating PrdIlKwang");
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro. Intente nuevamente.");

                // Repopulate dropdowns and return the view
                var dtoCat = await _prdNeveraService.GetCreateData();
              
                var operarios = await _userManager.GetUsersInRoleAsync("Operario");


                var modelDto = await _prdNeveraService.GetByIdAsync(model.Id);


                var vm = new PrdNeveraViewModel
                {
                    Id = modelDto.Id,
                    IdUsuarios = modelDto.IdUsuarios,
                    IdMaquina = modelDto.IdMaquina,
                    Fecha = modelDto.Fecha,
                    Observaciones = modelDto.Observaciones,
                    HoraInicio = modelDto.HoraInicio,
                    HoraFin = modelDto.HoraFin,
                    TiempoParo = modelDto.TiempoParo,
                    AprobadoSupervisor = modelDto.AprobadoSupervisor,
                    AprobadoGerencia = modelDto.AprobadoGerencia,
                    DetPrdNeveras = modelDto.DetPrdNeveras?.Select(x => new DetPrdNeveraViewModel
                    {
                        Posicion = x.Posicion,
                        IdArticulo = x.IdArticulo,
                        Articulo = dtoCat.CatalogoNeveras
                                      .FirstOrDefault(c => c.Id == x.IdArticulo)?
                                      .Descripcion,
                        CantidadConforme = x.CantidadConforme,
                        CantidadNoConforme = x.CantidadNoConforme,
                        IdTipoFabricacion = x.IdTipoFabricacion,
                        TipoFabricacion = dtoCat.CatTipoFabricacion
                                           .FirstOrDefault(t => t.Id == x.IdTipoFabricacion)?
                                           .Descripcion,
                        NumeroPedido = x.NumeroPedido
                    }).ToList(),
                    Articulos = dtoCat.CatalogoNeveras.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Codigo + "-" + a.Descripcion }),
                    Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                    Maquinas = dtoCat.CatMaquina.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                    TiposFabricacion = dtoCat.CatTipoFabricacion.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion })
                };

                return View(vm);

            }

        }

        // GET: PrdNeveraController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrdNeveraController/Delete/5
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
        private async Task PopulateDropdownListsAsync(PrdNeveraViewModel model)
        {
            var dtoCat = await _prdNeveraService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");
            model.Articulos =dtoCat.CatalogoNeveras.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Codigo+"-"+a.Descripcion });
            model.Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName });
            model.Maquinas = dtoCat.CatMaquina.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre });
            model.TiposFabricacion=dtoCat.CatTipoFabricacion.Select(tf => new SelectListItem { Value = tf.Id.ToString(), Text = tf.Descripcion });

        }
        //
        [HttpPost]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> ValidarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdNeveraService.ValidatePrdNeveraByIdAsync(id, userId);

            return Json(result);
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> AprobarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdNeveraService.AprovePrdNeveraByIdAsync(id, userId);

            return Json(result);
        }


        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrd(DetPrdNeveraViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetPrdNeveraDTO
            {
                Id = (int)model.detrpdId,
                IdArticulo = model.IdArticulo,
                CantidadConforme = model.CantidadConforme,
                CantidadNoConforme = model.CantidadNoConforme,
                IdTipoFabricacion = model.IdTipoFabricacion,
                NumeroPedido = model.NumeroPedido,
                IdUsuarioActualizacion = userId
            };

            await _prdNeveraService.UpdateDetPrd(dto);

            return Json(new { success = true, message = "Actualizacion exitosa!" });
        }

        public async Task<ActionResult> GetDataReport(DateTime start, DateTime end)
        {
            // Validate and ensure DateTime parameters are within valid SQL Server range
            start = ValidateDateTimeParameter(start);
            end = ValidateDateTimeParameter(end);
            
            var data = await _prdNeveraService.GetAllPrdNeveraWithDetailsAsync(start, end);
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
