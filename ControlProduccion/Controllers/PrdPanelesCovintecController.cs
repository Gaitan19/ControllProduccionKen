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
    public class PrdPanelesCovintecController : Controller
    {
        private readonly IPrdPanelesCovintecService _prdPanelesCovintecService;
        private readonly UserManager<IdentityUser> _userManager;
        public PrdPanelesCovintecController(IPrdPanelesCovintecService prdPanelesCovintecService, UserManager<IdentityUser> userManager)
        {
            _prdPanelesCovintecService = prdPanelesCovintecService;
            _userManager = userManager;
        }
        // GET: PrdPanelesCovintecController
        public async Task<ActionResult> Index()
        {
            var model = await _prdPanelesCovintecService.GetAllAsync();
         
            return View(model);
        }


        public async Task<ActionResult> GetDataReport(DateTime start, DateTime end)
        {
            // Validate and ensure DateTime parameters are within valid SQL Server range
            start = ValidateDateTimeParameter(start);
            end = ValidateDateTimeParameter(end);
            
            var data = await _prdPanelesCovintecService.GetAllPanelProduccionWithDetailsAsync(start,end);
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

        // GET: PrdPanelesCovintecController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var dtoCat = await _prdPanelesCovintecService.GetCreateData();
            // Obtener datos iniciales, incluyendo los catálogos
            var dto = await _prdPanelesCovintecService.GetCreateData();
            // Obtenemos los usuarios que tienen el rol "Operario"
            var userId = _userManager.GetUserId(User);
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");
            var model = await _prdPanelesCovintecService.GetByIdAsync(id);
            
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new PrdPanelesCovintecViewModel
            {
                //assign list of users with the operator role 

                Id = model.Id,
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                Fecha = (DateTime)model.Fecha,
                MermaAlambre = model.MermaAlambre,
                TiempoParo = model.TiempoParo,
                Observaciones = model.Observaciones,
                IdUsuarioCreacion = model.IdUsuarioCreacion,
                FechaCreacion = model.FechaCreacion,
                AprobadoGerencia=model.AprobadoGerencia,
                AprobadoSupervisor = model.AprobadoSupervisor,
                DetAlambrePrdPanelesCovintec = model.DetAlambrePrdPanelesCovintecs.Select(x => new DetAlambrePrdPanelesCovintecViewModel
                {
                    Id = x.Id,
                    PesoAlambre = x.PesoAlambre,
                    NumeroAlambre = x.NumeroAlambre,
                    AprobadoSupervisor = x.AprobadoSupervisor,
                    AprobadoGerencia = x.AprobadoGerencia

                }).ToList(),
                DetPrdPanelesCovintec = model.DetPrdPanelesCovintecs.Select(x => new DetPrdPanelesCovintecViewModel
                {
                    Id = x.Id,
                    IdArticulo = x.IdArticulo,
                    Articulo = dtoCat.CatalogoPaneles.FirstOrDefault(c => c.Id == x.IdArticulo)?.CodigoArticulo + "-" + dtoCat.CatalogoPaneles.FirstOrDefault(c => c.Id == x.IdArticulo)?.DescripcionArticulo,
                    CantidadProducida = x.CantidadProducida,
                    CantidadNoConforme = x.CantidadNoConforme,
                    IdTipoFabricacion = x.IdTipoFabricacion,
                    TipoFabricacion = dtoCat.CatTipoFabricacion.FirstOrDefault(c => c.Id == x.IdTipoFabricacion)?.Descripcion,
                    NumeroPedido = x.NumeroPedido,
                    AprobadoSupervisor = x.AprobadoSupervisor,
                    AprobadoGerencia = x.AprobadoGerencia
                }).ToList(),
                Operarios = operarios.Where(u => !u.LockoutEnd.HasValue || u.LockoutEnd.Value <= DateTimeOffset.Now).Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.UserName
                }),

                Maquinas = dto.CatMaquina.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nombre
                }),
                CatalogoPaneles = dto.CatalogoPaneles.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CodigoArticulo + '-' + m.DescripcionArticulo
                }),
                TiposFabricacion = dto.CatTipoFabricacion.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Descripcion
                }),


                // Puedes mapear otros campos si es necesario
            };


            return View(viewModel);
        }

   

      

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> ValidarPrdAlambreJefePrd(int id)
        {
            // Buscar el registro por el ID
            var alambre = await _prdPanelesCovintecService.AproveDetAlambrePrdPanelesCovintecByIdAsync(id, _userManager.GetUserId(User));


            // Retornar el nuevo valor booleano en formato JSON
            return Json(alambre);
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> ValidarPrdJefePrd(int id)
        {
            // Buscar el registro por el ID
            var prdVal = await _prdPanelesCovintecService.AproveDetPrdPanelesCovintecByIdAsync(id, _userManager.GetUserId(User));


            // Retornar el nuevo valor booleano en formato JSON
            return Json(prdVal);
        }

        // GET: PrdPanelesCovintecController/Create
        public async Task<ActionResult> Create()
        {
            // Obtener datos iniciales, incluyendo los catálogos
            var dto = await _prdPanelesCovintecService.GetCreateData();
            // Obtenemos los usuarios que tienen el rol "Operario"
            var userId = _userManager.GetUserId(User);
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");
         


            // Mapear DTO al ViewModel
            var viewModel = new PrdPanelesCovintecViewModel
            {
                //assign list of users with the operator role 


                Operarios = operarios.Where(u => !u.LockoutEnd.HasValue || u.LockoutEnd.Value <= DateTimeOffset.Now).Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.UserName
                }),

                Maquinas = dto.CatMaquina.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nombre
                }),
                CatalogoPaneles = dto.CatalogoPaneles.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CodigoArticulo+'-'+m.DescripcionArticulo
                }),
                TiposFabricacion = dto.CatTipoFabricacion.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Descripcion
                }),

                
            };

            return View(viewModel);

           
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrd(DetPrdPanelesCovintecViewModel model)
        {
            if (model.detrpdId == null)
            {
                return Json(new { success = false, message = "Error: ID de detalle no proporcionado." });
            }

            try
            {
                var userId = _userManager.GetUserId(User);
                var dto = new DetPrdPanelesCovintecDTO
                {
                    Id = model.detrpdId.Value,
                    IdArticulo = model.IdArticulo,
                    CantidadProducida = model.CantidadProducida,
                    CantidadNoConforme = model.CantidadNoConforme,
                    IdTipoFabricacion = model.IdTipoFabricacion,
                    NumeroPedido = model.NumeroPedido,
                    IdUsuarioActualizacion = userId
                };

                await _prdPanelesCovintecService.UpdateDetPrd(dto);
                return Json(new { success = true, message = "Actualización exitosa!" });
            }
            catch (KeyNotFoundException ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                // Log the exception here if logging is available
                return Json(new { success = false, message = "Ocurrió un error inesperado durante la actualización." });
            }
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> DeleteDetPrd(DetPrdPanelesCovintecViewModel model)
        {
            var dto = new DetPrdPanelesCovintecDTO
            {
                Id = model.Id,
                IdArticulo = model.IdArticulo,
                CantidadProducida = model.CantidadProducida,
                CantidadNoConforme = model.CantidadNoConforme,
                IdTipoFabricacion = model.IdTipoFabricacion,
                NumeroPedido = model.NumeroPedido,

            };

            await _prdPanelesCovintecService.DeleteDetPrd(dto);

            return Json(new { success = true, message = "Se elimino registro!" });
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetAlambrePrd(DetAlambrePrdPanelesCovintecViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetAlambrePrdPanelesCovintecDTO
            {
                Id = (int)model.detAlambreId,
                NumeroAlambre = model.NumeroAlambre,
                PesoAlambre = model.PesoAlambre,
               
                IdUsuarioActualizacion = userId
            };
            

            await _prdPanelesCovintecService.UpdateDetAlambrePrd(dto);

            return Json(new { success = true, message = "Actualizacion exitosa!" });
        }

        // POST: PrdPanelesCovintecController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] PrdPanelesCovintecViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            
            model.IdUsuarioCreacion = userId;
            model.IdTipoReporte = 9;

            var detallePrdPanelesCovintec = model.DetPrdPanelesCovintec.Select(x => new DetPrdPanelesCovintecDTO
            {
                IdArticulo = x.IdArticulo,
                CantidadProducida = x.CantidadProducida,
                CantidadNoConforme = x.CantidadNoConforme,
                IdTipoFabricacion = x.IdTipoFabricacion,
                NumeroPedido = x.NumeroPedido,
                AprobadoSupervisor = false,
                AprobadoGerencia=false,
                IdUsuarioCreacion = userId,

            }).ToList();

            var detalleAlambrePrdPanelesCovintec = model.DetAlambrePrdPanelesCovintec.Select(x => new DetAlambrePrdPanelesCovintecDTO
            {
                NumeroAlambre = x.NumeroAlambre,
                PesoAlambre = x.PesoAlambre,
               
                IdUsuarioCreacion = userId,
            }).ToList();

            var dto = new PrdPanelesCovintecDto
            {
                DetAlambrePrdPanelesCovintecs = detalleAlambrePrdPanelesCovintec,
                DetPrdPanelesCovintecs = detallePrdPanelesCovintec,
                IdUsuarios = model.IdUsuarios ,
                IdMaquina = model.IdMaquina,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                MermaAlambre = model.MermaAlambre,
                TiempoParo = model.TiempoParo,
                IdUsuarioCreacion = model.IdUsuarioCreacion
              
            };

            await _prdPanelesCovintecService.CreateAsync(dto);


            return Json(new { success = true, message = "Producción guardada!" });
        }

        // GET: PrdPanelesCovintecController/Edit/5
        public ActionResult Edit(int id, bool AprobadoJp, bool AprobadoSup)
        {
            var dtoCat = _prdPanelesCovintecService.GetCreateData().Result;
            // Obtener datos iniciales, incluyendo los catálogos
            var dto = _prdPanelesCovintecService.GetCreateData().Result;
            // Obtenemos los usuarios que tienen el rol "Operario"
            var userId = _userManager.GetUserId(User);
            var operarios = _userManager.GetUsersInRoleAsync("Operario").Result;
            var model = _prdPanelesCovintecService.GetByIdAsync(id).Result;

            var viewModel = new PrdPanelesCovintecViewModel
            {
                //assign list of users with the operator role 

                Id = model.Id,
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                Fecha = (DateTime)model.Fecha,
                Observaciones = model.Observaciones,
                MermaAlambre = model.MermaAlambre,
                TiempoParo = model.TiempoParo,
                IdUsuarioCreacion = model.IdUsuarioCreacion,
                FechaCreacion = model.FechaCreacion,
                AprobadoGerencia = model.AprobadoGerencia,
                AprobadoSupervisor = model.AprobadoSupervisor,
                DetAlambrePrdPanelesCovintec = model.DetAlambrePrdPanelesCovintecs.Select(x => new DetAlambrePrdPanelesCovintecViewModel
                {
                    Id = x.Id,
                    PesoAlambre = x.PesoAlambre,
                  
                    NumeroAlambre = x.NumeroAlambre,
                    AprobadoSupervisor = x.AprobadoSupervisor,
                    AprobadoGerencia = x.AprobadoGerencia

                }).ToList(),
                DetPrdPanelesCovintec = model.DetPrdPanelesCovintecs.Select(x => new DetPrdPanelesCovintecViewModel
                {
                   
                    Id = x.Id,
                    IdArticulo = x.IdArticulo,
                    Articulo = dtoCat.CatalogoPaneles.FirstOrDefault(c => c.Id == x.IdArticulo)?.CodigoArticulo+ "-"+dtoCat.CatalogoPaneles.FirstOrDefault(c => c.Id == x.IdArticulo)?.DescripcionArticulo,
                    CantidadProducida = x.CantidadProducida,
                    CantidadNoConforme = x.CantidadNoConforme,
                    IdTipoFabricacion = x.IdTipoFabricacion,
                    TipoFabricacion = dtoCat.CatTipoFabricacion.FirstOrDefault(c => c.Id == x.IdTipoFabricacion)?.Descripcion,
                    NumeroPedido = x.NumeroPedido,
                    ProduccionDia = x.ProduccionDia,
                    AprobadoSupervisor = x.AprobadoSupervisor,
                    AprobadoGerencia = x.AprobadoGerencia
                }).ToList(),
                Operarios = operarios.Where(u => !u.LockoutEnd.HasValue || u.LockoutEnd.Value <= DateTimeOffset.Now).Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.UserName
                }),

                Maquinas = dto.CatMaquina.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nombre
                }),
                CatalogoPaneles = dto.CatalogoPaneles.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CodigoArticulo + '-' + m.DescripcionArticulo
                }),
                TiposFabricacion = dto.CatTipoFabricacion.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Descripcion
                })

                // Puedes mapear otros campos si es necesario
            };


            return View(viewModel);
        }

        // POST: PrdPanelesCovintecController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> Edit(PrdPanelesCovintecViewModel model)
        {
           if(!ModelState.IsValid)
            {
                return   RedirectToAction(nameof(Index));
            }
            var userId = _userManager.GetUserId(User);

            var dto = new PrdPanelesCovintecDto
            {
                Id = model.Id,
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                IdUsuarioActualizacion = userId,
                FechaActualizacion = DateTime.Now,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                MermaAlambre=model.MermaAlambre,
                TiempoParo = model.TiempoParo

            };

            await _prdPanelesCovintecService.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }


        


        // GET: PrdPanelesCovintecController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrdPanelesCovintecController/Delete/5
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

        [HttpPost]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> ValidarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdPanelesCovintecService.ValidatePrdPanelCovintecByIdAsync(id, userId);

            return Json(result);
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> AprobarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdPanelesCovintecService.AprovePrdPanelCovintecByIdAsync(id, userId);

            return Json(result);
        }
    }
}
