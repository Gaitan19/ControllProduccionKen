using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Azure;
using ControlProduccion.ViewModel;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ControlProduccion.Controllers
{
    public class PrdCerchaCovintecController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPrdCerchaCovintecService _prdCerchaCovintecService;

        public PrdCerchaCovintecController(UserManager<IdentityUser> userManager, IPrdCerchaCovintecService prdCerchaCovintecService)
        {
            _userManager = userManager;
            _prdCerchaCovintecService = prdCerchaCovintecService;
        }
        // GET: PrdCerchaCovintecController
        public async Task<ActionResult> Index()
        {
            var model = await _prdCerchaCovintecService.GetAllAsync();
            return View(model);
        }

        // GET: PrdCerchaCovintecController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var dtoCat = await _prdCerchaCovintecService.GetCreateData();

            var userId = _userManager.GetUserId(User);
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var model = await _prdCerchaCovintecService.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
          
            var vm = new PrdCerchaCovintecViewModel
            {
                Id = model.Id,
                IdMaquina = model.IdMaquina,
                IdUsuarios = model.IdUsuarios,
                Fecha = (DateTime)model.Fecha,
                Observaciones = model.Observaciones,
                MermaAlambre = model.MermaAlambre,
                TiempoParo = model.TiempoParo,
                ConteoInicial = model.ConteoInicial,
                ConteoFinal = model.ConteoFinal,
                AprobadoGerencia = model.AprobadoGerencia,
                AprobadoSupervisor = model.AprobadoSupervisor,
                DetPrdCerchaCovintec = model.DetPrdCerchaCovintecs.Select(m => new DetPrdCerchaCovintecViewModel
                {
                    Id = m.Id,
                    Articulo = dtoCat.CatalogoCercha.FirstOrDefault(c => c.Id == m.IdArticulo)?.CodigoArticulo + "-" + dtoCat.CatalogoCercha.FirstOrDefault(c => c.Id == m.IdArticulo)?.DescripcionArticulo,
                    IdArticulo = m.IdArticulo,
                    CantidadProducida = m.CantidadProducida,
                    CantidadNoConforme = m.CantidadNoConforme,
                    IdTipoFabricacion = m.IdTipoFabricacion,
                    TipoFabricacion = dtoCat.CatTipoFabricacion.FirstOrDefault(c => c.Id == m.IdTipoFabricacion)?.Descripcion,
                    NumeroPedido = m.NumeroPedido,
                    AprobadoSupervisor = m.AprobadoSupervisor,
                    AprobadoGerencia = m.AprobadoGerencia
                }).ToList(),
                DetAlambrePrdCerchaCovintec = model.DetAlambrePrdCerchaCovintecs.Select(m => new DetAlambrePrdCerchaCovintecViewModel
                {
                    Id = m.Id,
                    NumeroAlambre = m.NumeroAlambre,
                    PesoAlambre = m.PesoAlambre,
                    AprobadoSupervisor = m.AprobadoSupervisor,
                    AprobadoGerencia = m.AprobadoGerencia
                }).ToList(),
                Operarios = operarios.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.UserName
                }),
                Maquinas = dtoCat.CatMaquina.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nombre
                }),
                CatalogoCercha = dtoCat.CatalogoCercha.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CodigoArticulo + '-' + m.DescripcionArticulo
                }),

                TiposFabricacion = dtoCat.CatTipoFabricacion.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Descripcion
                }),
            };
            return View(vm);
        }

        // GET: PrdCerchaCovintecController/Create
        public async Task<ActionResult> Create()
        {
            var model = await _prdCerchaCovintecService.GetCreateData();
            // Obtenemos los usuarios que tienen el rol "Operario"
            var userId = _userManager.GetUserId(User);
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var vm = new PrdCerchaCovintecViewModel
            {
                Operarios = operarios.Where(u => !u.LockoutEnd.HasValue || u.LockoutEnd.Value <= DateTimeOffset.Now).Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.UserName
                }),
                Maquinas = model.CatMaquina.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nombre
                }),
                CatalogoCercha = model.CatalogoCercha.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CodigoArticulo + '-' + m.DescripcionArticulo
                }),
                
                TiposFabricacion = model.CatTipoFabricacion.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Descripcion
                }),
            };


            return View(vm);
        }

        // POST: PrdCerchaCovintecController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] PrdCerchaCovintecViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            model.IdUsuarioCreacion = userId;
            model.IdTipoReporte = 7;

            var detPrdCercha=model.DetPrdCerchaCovintec.Select(x => new DetPrdCerchaCovintecDTO
            {
                IdArticulo = x.IdArticulo,
                CantidadProducida = x.CantidadProducida,
                CantidadNoConforme = x.CantidadNoConforme,
                IdTipoFabricacion = x.IdTipoFabricacion,
                NumeroPedido = x.NumeroPedido,
                AprobadoSupervisor = false,
                AprobadoGerencia = false,
                IdUsuarioCreacion = userId,
                FechaCreacion = DateTime.Now
            }).ToList();

            var detAlambre = model.DetAlambrePrdCerchaCovintec.Select(x => new DetAlambrePrdCerchaCovintecDTO
            {
                NumeroAlambre = x.NumeroAlambre,
                PesoAlambre = x.PesoAlambre,

                IdUsuarioCreacion = userId,
            }).ToList();

            var cerchaDTO = new PrdCerchaCovintecDTO
            {
                IdTipoReporte = model.IdTipoReporte,
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
               
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                MermaAlambre = model.MermaAlambre,
                TiempoParo = model.TiempoParo,
                IdUsuarioCreacion = model.IdUsuarioCreacion,
                ConteoInicial = model.ConteoInicial,
                ConteoFinal = model.ConteoFinal,
                DetPrdCerchaCovintecs = detPrdCercha,
                DetAlambrePrdCerchaCovintecs = detAlambre
            };

            await _prdCerchaCovintecService.CreateAsync(cerchaDTO);


            return Json(new { success = true, message = "Producción guardada!" });

        }

        // GET: PrdCerchaCovintecController/Edit/5
        public async Task<ActionResult> Edit(int id, bool AprobadoJp, bool AprobadoSup)
        {
            var dtoCat = await _prdCerchaCovintecService.GetCreateData();

            var userId = _userManager.GetUserId(User);
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var model = await _prdCerchaCovintecService.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var vm = new PrdCerchaCovintecViewModel
            {
                Id = model.Id,
                IdMaquina = model.IdMaquina,
                IdUsuarios = model.IdUsuarios,
                Fecha = (DateTime)model.Fecha,
                Observaciones = model.Observaciones,
                MermaAlambre = model.MermaAlambre,
                TiempoParo = model.TiempoParo,
                ConteoInicial = model.ConteoInicial,
                ConteoFinal = model.ConteoFinal,
                AprobadoGerencia = model.AprobadoGerencia,
                AprobadoSupervisor = model.AprobadoSupervisor,
                DetPrdCerchaCovintec = model.DetPrdCerchaCovintecs.Select(m => new DetPrdCerchaCovintecViewModel
                {
                    Id = m.Id,
                    Articulo = dtoCat.CatalogoCercha.FirstOrDefault(c => c.Id == m.IdArticulo)?.CodigoArticulo + "-" + dtoCat.CatalogoCercha.FirstOrDefault(c => c.Id == m.IdArticulo)?.DescripcionArticulo,
                    IdArticulo = m.IdArticulo,
                    CantidadProducida = m.CantidadProducida,
                    CantidadNoConforme = m.CantidadNoConforme,
                    IdTipoFabricacion = m.IdTipoFabricacion,
                    TipoFabricacion = dtoCat.CatTipoFabricacion.FirstOrDefault(c => c.Id == m.IdTipoFabricacion)?.Descripcion,
                    NumeroPedido = m.NumeroPedido,
                    AprobadoSupervisor = m.AprobadoSupervisor,
                    AprobadoGerencia = m.AprobadoGerencia,
                    ProduccionDia = m.ProduccionDia
                }).ToList(),
                DetAlambrePrdCerchaCovintec = model.DetAlambrePrdCerchaCovintecs.Select(m => new DetAlambrePrdCerchaCovintecViewModel
                {
                    Id = m.Id,
                    NumeroAlambre = m.NumeroAlambre,
                    PesoAlambre = m.PesoAlambre,
                    AprobadoSupervisor = m.AprobadoSupervisor,
                    AprobadoGerencia = m.AprobadoGerencia
                }).ToList(),
                Operarios = operarios.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.UserName
                }),
                Maquinas = dtoCat.CatMaquina.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nombre
                }),
                CatalogoCercha = dtoCat.CatalogoCercha.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.CodigoArticulo + '-' + m.DescripcionArticulo
                }),

                TiposFabricacion = dtoCat.CatTipoFabricacion.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Descripcion
                })
            };
            return View(vm);
        }

        // POST: PrdCerchaCovintecController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> Edit(PrdCerchaCovintecViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            var userId = _userManager.GetUserId(User);

            var dto = new PrdCerchaCovintecDTO
            {
                Id = model.Id,
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                IdUsuarioActualizacion = userId,
                FechaActualizacion = DateTime.Now,
                Fecha = model.Fecha,
                ConteoInicial = model.ConteoInicial,
                ConteoFinal = model.ConteoFinal,
                Observaciones = model.Observaciones,
                MermaAlambre = model.MermaAlambre,
                TiempoParo = model.TiempoParo

            };

            await _prdCerchaCovintecService.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        // GET: PrdCerchaCovintecController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrdCerchaCovintecController/Delete/5
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
        [Authorize(Roles = "JefeProduccion")]
        public IActionResult ValidarPrdAlambreJefePrd(int id)
        {
            // Buscar el registro por el ID
            var alambre = _prdCerchaCovintecService.AproveDetAlambrePrdCerchaCovintecByIdAsync(id, _userManager.GetUserId(User)).Result;


            // Retornar el nuevo valor booleano en formato JSON
            return Json(alambre);
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public IActionResult ValidarPrdJefePrd(int id)
        {
            // Buscar el registro por el ID
            var prdVal = _prdCerchaCovintecService.AproveDetPrdCerchaCovintecByIdAsync(id, _userManager.GetUserId(User)).Result;


            // Retornar el nuevo valor booleano en formato JSON
            return Json(prdVal);
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrd(DetPrdCerchaCovintecViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetPrdCerchaCovintecDTO
            {
                Id = (int)model.detrpdId,
                IdArticulo = model.IdArticulo,
                CantidadProducida = model.CantidadProducida,
                CantidadNoConforme = model.CantidadNoConforme,
                IdTipoFabricacion = model.IdTipoFabricacion,
                NumeroPedido = model.NumeroPedido,
                IdUsuarioActualizacion = userId

            };

            await _prdCerchaCovintecService.UpdateDetPrd(dto);

            return Json(new { success = true, message = "Actualizacion exitosa!" });
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetAlambrePrd(DetAlambrePrdCerchaCovintecViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetAlambrePrdCerchaCovintecDTO
            {
                Id = (int)model.detAlambreId,
                NumeroAlambre = model.NumeroAlambre,
                PesoAlambre = model.PesoAlambre,

                IdUsuarioActualizacion = userId
            };


            await _prdCerchaCovintecService.UpdateDetAlambrePrd(dto);

            return Json(new { success = true, message = "Actualizacion exitosa!" });
        }

        public ActionResult GetDataReport(DateTime start, DateTime end)
        {
            // Validate and ensure DateTime parameters are within valid SQL Server range
            start = ValidateDateTimeParameter(start);
            end = ValidateDateTimeParameter(end);
            
            var data = _prdCerchaCovintecService.GetAllCerchaProduccionReporteWithDetailsAsync(start, end).Result;
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

        [HttpPost]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> ValidarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdCerchaCovintecService.ValidatePrdCechaCovintecByIdAsync(id, userId);

            return Json(result);
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> AprobarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdCerchaCovintecService.AprovePrdCerchaCovintecByIdAsync(id, userId);

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> SaveNotaSupervisor(int id, string notaSupervisor)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdCerchaCovintecService.UpdateNotaSupervisorAsync(id, notaSupervisor, userId);
            
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
