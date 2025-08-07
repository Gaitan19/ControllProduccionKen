using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using ControlProduccion.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ControlProduccion.Controllers
{
    [Authorize]
    public class PrdMallasCovintecController : Controller
    {
        private readonly IPrdMallasCovintecService _prdMallasCovintecService;
        private readonly UserManager<IdentityUser> _userManager;

        public PrdMallasCovintecController(
            IPrdMallasCovintecService prdMallasCovintecService,
            UserManager<IdentityUser> userManager)
        {
            _prdMallasCovintecService = prdMallasCovintecService;
            _userManager = userManager;
        }

        // GET: PrdMallasCovintec
        public async Task<ActionResult> Index()
        {
            var model = await _prdMallasCovintecService.GetAllAsync();
            return View(model);
        }

        // GET: PrdMallasCovintec/ReportData?start=...&end=...
        public async Task<ActionResult> GetDataReport(DateTime start, DateTime end)
        {
            var data = await _prdMallasCovintecService
                .GetAllMallasProduccionWithDetailsAsync(start, end);
            return View(data);
        }

        // GET: PrdMallasCovintec/Details/5
        public async Task<ActionResult> Details(int id)
        {
            // Catálogos y usuarios
            var dtoCat = await _prdMallasCovintecService.GetCreateData();
            var operarios = await _userManager
                .GetUsersInRoleAsync("Operario");
                

            // Datos de la entidad
            var modelDto = await _prdMallasCovintecService.GetByIdAsync(id);
            if (modelDto == null)
            {
                return NotFound();
            }

            var vm = new PrdMallasCovintecViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                IdMaquina = modelDto.IdMaquina,
                Fecha = modelDto.Fecha ?? DateTime.Now,
                Observaciones = modelDto.Observaciones,
                MermaAlambre = modelDto.MermaAlambre,
                TiempoParo = modelDto.TiempoParo,
                IdUsuarioCreacion = modelDto.IdUsuarioCreacion,
                FechaCreacion = modelDto.FechaCreacion,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,

                Operarios = operarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.UserName
                }),

                Maquinas = dtoCat.CatMaquina.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nombre
                }),

                CatalogoMallas = dtoCat.CatalogoMallas.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.CodigoArticulo}-{c.DescripcionArticulo}"
                }),

                TiposFabricacion = dtoCat.CatTipoFabricacion.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Descripcion
                }),

                DetPrdMallaCovintec = modelDto.DetPrdMallasCovintecs?.Select(x => new DetPrdMallaCovintecViewModel
                {
                    Id = x.Id,
                    IdArticulo = x.IdArticulo,
                    Articulo = dtoCat.CatalogoMallas
                                  .FirstOrDefault(c => c.Id == x.IdArticulo)?
                                  .DescripcionArticulo,
                    CantidadProducida = x.CantidadProducida,
                    CantidadNoConforme = x.CantidadNoConforme,
                    IdTipoFabricacion = x.IdTipoFabricacion,
                    TipoFabricacion = dtoCat.CatTipoFabricacion
                                       .FirstOrDefault(t => t.Id == x.IdTipoFabricacion)?
                                       .Descripcion,
                    NumeroPedido = x.NumeroPedido,
                    ProduccionDia = x.ProduccionDia,
                    AprobadoSupervisor = x.AprobadoSupervisor,
                    AprobadoGerencia = x.AprobadoGerencia
                }).ToList(),

                DetAlambrePrdMallaCovintec = modelDto.DetAlambrePrdMallasCovintecs?.Select(x => new DetAlambrePrdMallasCovintecViewModel
                {
                    Id = x.Id,
                    NumeroAlambre = x.NumeroAlambre,
                    PesoAlambre = x.PesoAlambre,
                    AprobadoSupervisor = x.AprobadoSupervisor,
                    AprobadoGerencia = x.AprobadoGerencia
                }).ToList()
            };

            return View(vm);
        }

      

 

        // GET: PrdMallasCovintec/Create
        public async Task<ActionResult> Create()
        {
            var dto = await _prdMallasCovintecService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var vm = new PrdMallasCovintecViewModel
            {
                Operarios = operarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.UserName
                }),
                Maquinas = dto.CatMaquina.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nombre
                }),
                CatalogoMallas = dto.CatalogoMallas.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.CodigoArticulo}-{c.DescripcionArticulo}"
                }),
                TiposFabricacion = dto.CatTipoFabricacion.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Descripcion
                })
            };

            return View(vm);
        }

        // POST: PrdMallasCovintec/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] PrdMallasCovintecViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _userManager.GetUserId(User);
            model.IdUsuarioCreacion = userId;
            model.IdTipoReporte = 9;

            var detallePrd = model.DetPrdMallaCovintec?.Select(x => new DetPrdMallaCovintecDTO
            {
                IdArticulo = x.IdArticulo,
                CantidadProducida = x.CantidadProducida,
                CantidadNoConforme = x.CantidadNoConforme,
                IdTipoFabricacion = x.IdTipoFabricacion,
                NumeroPedido = x.NumeroPedido,
                IdUsuarioCreacion = userId
            }).ToList();

            var detalleAl = model.DetAlambrePrdMallaCovintec?.Select(x => new DetAlambrePrdMallaCovintecDTO
            {
                NumeroAlambre = x.NumeroAlambre,
                PesoAlambre = x.PesoAlambre,
                IdUsuarioCreacion = userId
            }).ToList();

            var dto = new PrdMallaCovintecDto
            {
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                MermaAlambre = model.MermaAlambre,
                TiempoParo = model.TiempoParo,
                IdUsuarioCreacion = model.IdUsuarioCreacion!,
                DetPrdMallasCovintecs = detallePrd,
                DetAlambrePrdMallasCovintecs = detalleAl
            };

            await _prdMallasCovintecService.CreateAsync(dto);
            return Json(new { success = true, message = "Producción guardada!" });
        }

        // GET: PrdMallasCovintec/Edit/5
        public ActionResult Edit(int id, bool AprobadoJp = false, bool AprobadoSup = false)
        {
            var dtoCat = _prdMallasCovintecService.GetCreateData().Result;
            var operarios = _userManager.GetUsersInRoleAsync("Operario").Result;
                
            var modelDto = _prdMallasCovintecService.GetByIdAsync(id).Result;

            var vm = new PrdMallasCovintecViewModel
            {
                Id = modelDto.Id,
                IdUsuarios = modelDto.IdUsuarios,
                IdMaquina = modelDto.IdMaquina,
                Fecha = modelDto.Fecha ?? DateTime.Now,
                Observaciones = modelDto.Observaciones,
                MermaAlambre = modelDto.MermaAlambre,
                TiempoParo = modelDto.TiempoParo,
                IdUsuarioCreacion = modelDto.IdUsuarioCreacion,
                FechaCreacion = modelDto.FechaCreacion,
                AprobadoGerencia = modelDto.AprobadoGerencia,
                AprobadoSupervisor = modelDto.AprobadoSupervisor,

                Operarios = operarios.Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.UserName
                }),
                Maquinas = dtoCat.CatMaquina.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nombre
                }),
                CatalogoMallas = dtoCat.CatalogoMallas.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.CodigoArticulo}-{c.DescripcionArticulo}"
                }),
                TiposFabricacion = dtoCat.CatTipoFabricacion.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Descripcion
                }),

                DetPrdMallaCovintec = modelDto.DetPrdMallasCovintecs?.Select(x => new DetPrdMallaCovintecViewModel
                {
                    Id = x.Id,
                    IdArticulo = x.IdArticulo,
                    Articulo = dtoCat.CatalogoMallas
                                   .FirstOrDefault(c => c.Id == x.IdArticulo)?
                                   .DescripcionArticulo,
                    CantidadProducida = x.CantidadProducida,
                    CantidadNoConforme = x.CantidadNoConforme,
                    IdTipoFabricacion = x.IdTipoFabricacion,
                    TipoFabricacion = dtoCat.CatTipoFabricacion
                                       .FirstOrDefault(t => t.Id == x.IdTipoFabricacion)?
                                       .Descripcion,
                    NumeroPedido = x.NumeroPedido,
                    ProduccionDia = x.ProduccionDia,
                    AprobadoSupervisor = x.AprobadoSupervisor,
                    AprobadoGerencia = x.AprobadoGerencia
                }).ToList(),

                DetAlambrePrdMallaCovintec = modelDto.DetAlambrePrdMallasCovintecs?.Select(x => new DetAlambrePrdMallasCovintecViewModel
                {
                    Id = x.Id,
                    NumeroAlambre = x.NumeroAlambre,
                    PesoAlambre = x.PesoAlambre,
                    AprobadoSupervisor = x.AprobadoSupervisor,
                    AprobadoGerencia = x.AprobadoGerencia
                }).ToList()
            };

            return View(vm);
        }

        // POST: PrdMallasCovintec/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> Edit(PrdMallasCovintecViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            var userId = _userManager.GetUserId(User);
            var dto = new PrdMallaCovintecDto
            {
                Id = model.Id,
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                Fecha = model.Fecha,
                Observaciones = model.Observaciones,
                MermaAlambre = model.MermaAlambre,
                TiempoParo = model.TiempoParo,
                IdUsuarioActualizacion = userId
            };

            await _prdMallasCovintecService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: PrdMallasCovintec/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrdMallasCovintec/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            // Aquí podrías implementar la eliminación completa del registro si hace falta
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrd(DetPrdMallaCovintecViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetPrdMallaCovintecDTO
            {
                Id = (int)model.detrpdId,
                IdArticulo = model.IdArticulo,
                CantidadProducida = model.CantidadProducida,
                CantidadNoConforme = model.CantidadNoConforme,
                IdTipoFabricacion = model.IdTipoFabricacion,
                NumeroPedido = model.NumeroPedido,
                IdUsuarioActualizacion = userId
            };

            await _prdMallasCovintecService.UpdateDetPrd(dto);

            return Json(new { success = true, message = "Actualizacion exitosa!" });
        }


        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetAlambrePrd(DetAlambrePrdMallasCovintecViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var dto = new DetAlambrePrdMallaCovintecDTO
            {
                Id = (int)model.detAlambreId,
                NumeroAlambre = model.NumeroAlambre,
                PesoAlambre = model.PesoAlambre,

                IdUsuarioActualizacion = userId
            };


            await _prdMallasCovintecService.UpdateDetAlambrePrd(dto);

            return Json(new { success = true, message = "Actualizacion exitosa!" });
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> ValidarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdMallasCovintecService.ValidatePrdMallaCovintecByIdAsync(id, userId);

            return Json(result);
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> AprobarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdMallasCovintecService.AprovePrdMallaCovintecByIdAsync(id, userId);

            return Json(result);
        }
    }
}
