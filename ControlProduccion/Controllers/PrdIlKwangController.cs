using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using ControlProduccion.ViewModel;
using Infrastructure.DTO;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ControlProduccion.Controllers
{
    [Authorize]
    public class PrdIlKwangController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPrdIlKwangService _prdIlKwangService;

        public PrdIlKwangController( 
            UserManager<IdentityUser> userManager,
            IPrdIlKwangService prdIlKwangService
            )
        {
              _prdIlKwangService = prdIlKwangService;
            _userManager = userManager;
        }

        /// <summary>
        /// Helper method to populate dropdown lists for the view model
        /// </summary>
        private async Task PopulateDropdownListsAsync(PrdIlKwangViewModel model)
        {
            var dtoCat = await _prdIlKwangService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");
              

            model.Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName });
            model.Maquinas = dtoCat.CatMaquina.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre });
            model.CatalogoArticulos = dtoCat.CatTermoIsoPanel.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.DescripcionArticulo });
            model.TiposFabricacion = dtoCat.CatTipoFabricacion.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion });
            model.CatEspesor = dtoCat.CatEspesor.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Valor });
            model.CatStatus = dtoCat.CatStatus.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Descripcion });
            model.CatTipo = dtoCat.CatTipo.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion });
            model.AnchosBobinaA = dtoCat.CatAnchoBobina.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Valor.ToString() });
            model.AnchosBobinaB = dtoCat.CatAnchoBobina.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Valor.ToString() });
            model.ColoresBobinaA = dtoCat.CatColoresBobina.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Color });
            model.ColoresBobinaB = dtoCat.CatColoresBobina.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Color });
            model.UbicacionesBobinaA = dtoCat.CatUbicacionBobina.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Descripcion });
            model.UbicacionesBobinaB = dtoCat.CatUbicacionBobina.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Descripcion });
        }
        // GET: PrdIlKwangController
        public async Task<ActionResult> Index()
        {
            var model = await _prdIlKwangService.GetAllAsync();
            return View(model);
        }

        // GET: PrdIlKwangController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var dtCat = await _prdIlKwangService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");
            var userId = _userManager.GetUserId(User);
            var model = await _prdIlKwangService.GetByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new PrdIlKwangViewModel
            {
                Id = model.Id,
                IdTipoReporte = model.IdTipoReporte,
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                Fecha = model.Fecha,
                HoraInicio = model.HoraInicio,
                HoraFin = model.HoraFin,
                TiempoParo = model.TiempoParo ?? 0,
                IdArticulo = model.IdArticulo ?? new List<string>(),
                IdTipoFabricacion = model.IdTipoFabricacion,
                Cliente = model.Cliente,
                NumeroPedido = model.NumeroPedido,
                VelocidadMaquina = model.VelocidadMaquina,
                FabricanteBobinaA = model.FabricanteBobinaA,
                CodigoBobinaA = model.CodigoBobinaA,
                CalibreMmA = model.CalibreMmA,
                AnchoMmA = model.AnchoMmA,
                PesoInicialKgA = model.PesoInicialKgA,
                PesoFinalKgA = model.PesoFinalKgA,
                MetrosInicialA = model.MetrosInicialA,
                MetrosFinalA = model.MetrosFinalA,
                EspesorInicialCmA = model.EspesorInicialCmA,
                EspesorFinalCmA = model.EspesorFinalCmA,
                //ConsumoBobinaKgA = model.ConsumoBobinaKgA,
                FabricanteBobinaB = model.FabricanteBobinaB,
                CodigoBobinaB = model.CodigoBobinaB,
                CalibreMmB = model.CalibreMmB,
                AnchoMmB = model.AnchoMmB,
                PesoInicialKgB = model.PesoInicialKgB,
                PesoFinalKgB = model.PesoFinalKgB,
                MetrosInicialB = model.MetrosInicialB,
                MetrosFinalB = model.MetrosFinalB,
                EspesorInicialCmB = model.EspesorInicialCmB,
                EspesorFinalCmB = model.EspesorFinalCmB,
                //ConsumoBobinaKgB = model.ConsumoBobinaKgB,
                PesoInicialA = model.PesoInicialA,
                CantidadUtilizadaA = model.CantidadUtilizadaA,
                PesoFinalA = model.PesoFinalA,
                VelocidadSuperiorA = model.VelocidadSuperiorA,
                VelocidadInferiorA = model.VelocidadInferiorA,
                LoteA = model.LoteA,
                VencimientoA = model.VencimientoA,
                PesoInicialB = model.PesoInicialB,
                CantidadUtilizadaB = model.CantidadUtilizadaB,
                PesoFinalB = model.PesoFinalB,
                VelocidadSuperiorB = model.VelocidadSuperiorB,
                VelocidadInferiorB = model.VelocidadInferiorB,
                LoteB = model.LoteB,
                VencimientoB = model.VencimientoB,
                IdUbicacionBobinaA = model.IdUbicacionBobinaA,
                IdAnchoBobinaA = model.IdAnchoBobinaA,
                IdColorBobinaA = model.IdColorBobinaA,
                IdUbicacionBobinaB = model.IdUbicacionBobinaB,
                IdAnchoBobinaB = model.IdAnchoBobinaB,
                IdColorBobinaB = model.IdColorBobinaB,
                CantidadArranques = model.CantidadArranques ?? 0,
                IdUsuarioCreacion = model.IdUsuarioCreacion,
                IdUsuarioActualizacion = model.IdUsuarioActualizacion,
                AprobadoSupervisor = model.AprobadoSupervisor,
                AprobadoGerencia = model.AprobadoGerencia,
                Observaciones = model.Observaciones,

                // Catalogos
                Maquinas = dtCat.CatMaquina.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre }),
                Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName }),
                CatalogoArticulos = dtCat.CatTermoIsoPanel.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.DescripcionArticulo }),
                TiposFabricacion = dtCat.CatTipoFabricacion.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion }),
                CatEspesor = dtCat.CatEspesor.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Valor }),
                CatStatus = dtCat.CatStatus.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Descripcion }),
                CatTipo = dtCat.CatTipo.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion }),
                AnchosBobinaA = dtCat.CatAnchoBobina.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Valor.ToString() }),
                AnchosBobinaB = dtCat.CatAnchoBobina.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Valor.ToString() }),
                ColoresBobinaA = dtCat.CatColoresBobina.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Color }),
                ColoresBobinaB = dtCat.CatColoresBobina.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Color }),
                UbicacionesBobinaA = dtCat.CatUbicacionBobina.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Descripcion }),
                UbicacionesBobinaB = dtCat.CatUbicacionBobina.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Descripcion }),

                // Detalles
                DetPrdIlKwangs = model.DetPrdIlKwangs?.Select(d => new DetPrdIlKwangViewModel
                {
                    Id = d.Id,
                    PrdIlKwangId = d.PrdIlKwangId,
                    Posicion = d.Posicion,
                    IdEspesor = d.IdEspesor,
                    Cantidad = d.Cantidad,
                    Medida = d.Medida,
                    MetrosCuadrados = d.MetrosCuadrados,
                    IdStatus = d.IdStatus,
                    IdTipo = d.IdTipo,
                    IdUsuarioCreacion = d.IdUsuarioCreacion,
                    IdUsuarioActualizacion = d.IdUsuarioActualizacion
                }).ToList() ?? new List<DetPrdIlKwangViewModel>()
            };


            return View(viewModel);
        }

        // GET: PrdIlKwangController/Create
        public async Task<ActionResult> Create()
        {
            var dto = await _prdIlKwangService.GetCreateData();
            var operarios = await _userManager.GetUsersInRoleAsync("Operario");

            var vm = new PrdIlKwangViewModel
            {
                Maquinas = dto.CatMaquina.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Nombre
                }),
                Operarios = operarios.Select(o => new SelectListItem
                {
                    Value = o.Id,
                    Text = o.UserName
                }),
                CatalogoArticulos=dto.CatTermoIsoPanel.Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.DescripcionArticulo
                }),
                TiposFabricacion = dto.CatTipoFabricacion.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Descripcion
                }),
                 CatEspesor=dto.CatEspesor.Select(e => new SelectListItem
                 {
                     Value = e.Id.ToString(),
                    Text = e.Valor
                 }),
                 CatStatus=dto.CatStatus.Select(s => new SelectListItem
                 {
                     Value = s.Id.ToString(),
                     Text = s.Descripcion
                 }),
                 CatTipo= dto.CatTipo.Select(t => new SelectListItem
                 {
                     Value = t.Id.ToString(),
                     Text = t.Descripcion
                 }),
                 AnchosBobinaA=dto.CatAnchoBobina.Select(a => new SelectListItem
                 {
                     Value = a.Id.ToString(),
                     Text = a.Valor.ToString()
                 }),
                    AnchosBobinaB=dto.CatAnchoBobina.Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Valor.ToString()
                    }),
                    ColoresBobinaA=dto.CatColoresBobina.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Color
                    }),
                    ColoresBobinaB=dto.CatColoresBobina.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Color
                    }),
                    UbicacionesBobinaA=dto.CatUbicacionBobina.Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.Descripcion
                    }),
                    UbicacionesBobinaB=dto.CatUbicacionBobina.Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.Descripcion
                    })

            };

            return View(vm);
        }

        // POST: PrdIlKwangController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] PrdIlKwangViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = _userManager.GetUserId(User);
            model.IdUsuarioCreacion = userId;
            model.IdTipoReporte = 6;

            var detPrdIlKwangDTO = model.DetPrdIlKwangs?.Select(d => new DetPrdIlKwangDTO
            {
                
                IdEspesor = d.IdEspesor,
                Cantidad = d.Cantidad,
                Medida = d.Medida,
                MetrosCuadrados = d.Cantidad*d.Medida,
                IdStatus = d.IdStatus,
                IdTipo = d.IdTipo,
                Posicion = (int)d.Posicion
            }).ToList();

            var dto = new PrdIlKwangDTO
            {
                IdMaquina = model.IdMaquina,
                IdUsuarios = model.IdUsuarios,
                Fecha = model.Fecha,
                HoraInicio = model.HoraInicio,
                HoraFin = model.HoraFin,
                IdAnchoBobinaA = model.IdAnchoBobinaA,
                IdAnchoBobinaB = model.IdAnchoBobinaB,
                IdColorBobinaA = model.IdColorBobinaA,
                IdColorBobinaB = model.IdColorBobinaB,
                IdUbicacionBobinaA = model.IdUbicacionBobinaA,
                IdUbicacionBobinaB = model.IdUbicacionBobinaB,
                PesoInicialA = model.PesoInicialA,
                CantidadUtilizadaA = model.CantidadUtilizadaA,
                PesoFinalA = model.PesoFinalA,
                VelocidadSuperiorA = model.VelocidadSuperiorA,
                VelocidadInferiorA = model.VelocidadInferiorA,
                LoteA = model.LoteA,
                VencimientoA = model.VencimientoA,
                PesoInicialB = model.PesoInicialB,
                CantidadUtilizadaB = model.CantidadUtilizadaB,
                PesoFinalB = model.PesoFinalB,
                VelocidadSuperiorB = model.VelocidadSuperiorB,
                VelocidadInferiorB = model.VelocidadInferiorB,
                LoteB = model.LoteB,
                VencimientoB = model.VencimientoB,
                Observaciones = model.Observaciones,
                CantidadArranques = model.CantidadArranques,
                DetPrdIlKwangs=detPrdIlKwangDTO,
                IdTipoReporte = model.IdTipoReporte,
                IdArticulo = model.IdArticulo,
                IdTipoFabricacion = model.IdTipoFabricacion,
                Cliente = model.Cliente,
                NumeroPedido= model.NumeroPedido,
                VelocidadMaquina= model.VelocidadMaquina,
                FabricanteBobinaA= model.FabricanteBobinaA,
                FabricanteBobinaB= model.FabricanteBobinaB,
                CodigoBobinaA = model.CodigoBobinaA,
                CalibreMmA = model.CalibreMmA,
                AnchoMmA= model.AnchoMmA,
                PesoInicialKgA= model.PesoInicialKgA,
                PesoFinalKgA= model.PesoFinalKgA,
                MetrosInicialA=model.MetrosInicialA,
                MetrosFinalA=model.MetrosFinalA,
                EspesorInicialCmA=model.EspesorInicialCmA,
                EspesorFinalCmA=model.EspesorFinalCmA,
                ConsumoBobinaKgA=model.PesoInicialKgA-model.PesoFinalKgA,
                CodigoBobinaB=model.CodigoBobinaB,
                CalibreMmB= model.CalibreMmB,
                AnchoMmB=model.AnchoMmB,
                PesoInicialKgB=model.PesoInicialKgB,
                PesoFinalKgB=model.PesoFinalKgB,
                MetrosInicialB= model.MetrosInicialB,
                MetrosFinalB= model.MetrosFinalB,
                EspesorInicialCmB= model.EspesorInicialCmB,
                EspesorFinalCmB= model.EspesorFinalCmB,
                ConsumoBobinaKgB= model.PesoInicialKgB - model.PesoFinalKgB,
                IdUsuarioCreacion=model.IdUsuarioCreacion,
                FechaCreacion= DateTime.Now,
                AprobadoGerencia=false,
                AprobadoSupervisor=false,
                TiempoParo= model.TiempoParo
            };

            await _prdIlKwangService.CreateAsync(dto);
            return Json(new { success = true, message = "Producción guardada!" });
        }

        // GET: PrdIlKwangController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var dtoCat = await _prdIlKwangService.GetCreateData();
            var operarios = (await _userManager.GetUsersInRoleAsync("Operario"))
                .Where(u => u.Id != _userManager.GetUserId(User));
            var model = await _prdIlKwangService.GetByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            var vm = new PrdIlKwangViewModel
            {
                Id = model.Id,
                IdUsuarios = model.IdUsuarios,
                IdMaquina = model.IdMaquina,
                HoraInicio = model.HoraInicio,
                HoraFin = model.HoraFin,
                Fecha = model.Fecha,
                TiempoParo = model.TiempoParo ?? 0,
                IdArticulo = model.IdArticulo ?? new List<string>(),
                IdTipoFabricacion = model.IdTipoFabricacion,
                Cliente = model.Cliente,
                NumeroPedido = model.NumeroPedido,
                VelocidadMaquina = model.VelocidadMaquina,
                IdUbicacionBobinaA = model.IdUbicacionBobinaA,
                IdAnchoBobinaA = model.IdAnchoBobinaA,
                FabricanteBobinaA = model.FabricanteBobinaA,
                CodigoBobinaA = model.CodigoBobinaA,
                CalibreMmA = model.CalibreMmA,
                IdColorBobinaA = model.IdColorBobinaA,
                AnchoMmA = model.AnchoMmA,
                PesoInicialKgA = model.PesoInicialKgA,
                PesoFinalKgA = model.PesoFinalKgA,
                MetrosInicialA = model.MetrosInicialA,
                MetrosFinalA = model.MetrosFinalA,
                EspesorInicialCmA = model.EspesorInicialCmA,
                EspesorFinalCmA = model.EspesorFinalCmA,
                FabricanteBobinaB = model.FabricanteBobinaB,
                CodigoBobinaB = model.CodigoBobinaB,
                CalibreMmB = model.CalibreMmB,
                AnchoMmB = model.AnchoMmB,
                PesoInicialKgB = model.PesoInicialKgB,
                PesoFinalKgB = model.PesoFinalKgB,
                MetrosInicialB = model.MetrosInicialB,
                MetrosFinalB = model.MetrosFinalB,
                EspesorInicialCmB = model.EspesorInicialCmB,
                EspesorFinalCmB = model.EspesorFinalCmB,
                PesoInicialA = model.PesoInicialA,
                CantidadUtilizadaA = model.CantidadUtilizadaA,
                PesoFinalA = model.PesoFinalA,
                VelocidadSuperiorA = model.VelocidadSuperiorA,
                VelocidadInferiorA = model.VelocidadInferiorA,
                LoteA = model.LoteA,
                VencimientoA = model.VencimientoA,
                PesoInicialB = model.PesoInicialB,
                CantidadUtilizadaB = model.CantidadUtilizadaB,
                PesoFinalB = model.PesoFinalB,
                VelocidadSuperiorB = model.VelocidadSuperiorB,
                VelocidadInferiorB = model.VelocidadInferiorB,
                LoteB = model.LoteB,
                VencimientoB = model.VencimientoB,
                Observaciones = model.Observaciones,
                CantidadArranques = model.CantidadArranques ?? 0,
                IdUsuarioCreacion = model.IdUsuarioCreacion,
                FechaCreacion = model.FechaCreacion,
                AprobadoGerencia = model.AprobadoGerencia,
                AprobadoSupervisor = model.AprobadoSupervisor,
                DetPrdIlKwangs = model.DetPrdIlKwangs?.Select(x => new DetPrdIlKwangViewModel
                {
                    Id = x.Id,
                    detrpdId = x.Id,
                    PrdIlKwangId = x.PrdIlKwangId,
                    Posicion = x.Posicion,
                    IdEspesor = x.IdEspesor,
                    Cantidad = x.Cantidad,
                    Medida = x.Medida,
                    MetrosCuadrados = x.MetrosCuadrados,
                    IdStatus = x.IdStatus,
                    IdTipo = x.IdTipo
                }).ToList()
            };

            vm.Operarios = operarios.Select(o => new SelectListItem { Value = o.Id, Text = o.UserName });
            vm.Maquinas = dtoCat.CatMaquina.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Nombre });
            vm.CatalogoArticulos = dtoCat.CatTermoIsoPanel.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.DescripcionArticulo });
            vm.TiposFabricacion = dtoCat.CatTipoFabricacion.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion });
            vm.CatEspesor = dtoCat.CatEspesor.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Valor });
            vm.CatStatus = dtoCat.CatStatus.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Descripcion });
            vm.CatTipo = dtoCat.CatTipo.Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Descripcion });
            vm.AnchosBobinaA = dtoCat.CatAnchoBobina.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Valor.ToString() });
            vm.AnchosBobinaB = dtoCat.CatAnchoBobina.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Valor.ToString() });
            vm.ColoresBobinaA = dtoCat.CatColoresBobina.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Color });
            vm.ColoresBobinaB = dtoCat.CatColoresBobina.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Color });
            vm.UbicacionesBobinaA = dtoCat.CatUbicacionBobina.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Descripcion });
            vm.UbicacionesBobinaB = dtoCat.CatUbicacionBobina.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Descripcion });
            
            return View(vm);
        }

        // POST: PrdIlKwangController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> Edit(PrdIlKwangViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate SelectListItem properties using helper method
                await PopulateDropdownListsAsync(model);
                
                // Repopulate detail items
                var det = await _prdIlKwangService.GetByIdAsync(model.Id);
                model.DetPrdIlKwangs = det?.DetPrdIlKwangs?.Select(x => new DetPrdIlKwangViewModel
                {
                    Id = x.Id,
                    detrpdId = x.Id,
                    PrdIlKwangId = x.PrdIlKwangId,
                    Posicion = x.Posicion,
                    IdEspesor = x.IdEspesor,
                    Cantidad = x.Cantidad,
                    Medida = x.Medida,
                    MetrosCuadrados = x.MetrosCuadrados,
                    IdStatus = x.IdStatus,
                    IdTipo = x.IdTipo
                }).ToList() ?? new List<DetPrdIlKwangViewModel>();
                return View(model);
            }

            try
            {
                var userId = _userManager.GetUserId(User);
                var dto = new PrdIlKwangDTO
                {
                    Id = model.Id,
                    IdUsuarios = model.IdUsuarios,
                    IdMaquina = model.IdMaquina,
                    HoraInicio = model.HoraInicio,
                    HoraFin = model.HoraFin,
                    Fecha = model.Fecha,
                    TiempoParo = model.TiempoParo,
                    IdArticulo = model.IdArticulo,
                    IdTipoFabricacion = model.IdTipoFabricacion,
                    Cliente = model.Cliente,
                    NumeroPedido = model.NumeroPedido,
                    VelocidadMaquina = model.VelocidadMaquina,
                    IdUbicacionBobinaA = model.IdUbicacionBobinaA,
                    IdAnchoBobinaA = model.IdAnchoBobinaA,
                    FabricanteBobinaA = model.FabricanteBobinaA,
                    CodigoBobinaA = model.CodigoBobinaA,
                    CalibreMmA = model.CalibreMmA,
                    IdColorBobinaA = model.IdColorBobinaA,
                    AnchoMmA = model.AnchoMmA,
                    PesoInicialKgA = model.PesoInicialKgA,
                    PesoFinalKgA = model.PesoFinalKgA,
                    MetrosInicialA = model.MetrosInicialA,
                    MetrosFinalA = model.MetrosFinalA,
                    EspesorInicialCmA = model.EspesorInicialCmA,
                    EspesorFinalCmA = model.EspesorFinalCmA,
                    IdUbicacionBobinaB = model.IdUbicacionBobinaB,
                    IdAnchoBobinaB = model.IdAnchoBobinaB,
                    FabricanteBobinaB = model.FabricanteBobinaB,
                    CodigoBobinaB = model.CodigoBobinaB,
                    CalibreMmB = model.CalibreMmB,
                    IdColorBobinaB = model.IdColorBobinaB,
                    AnchoMmB = model.AnchoMmB,
                    PesoInicialKgB = model.PesoInicialKgB,
                    PesoFinalKgB = model.PesoFinalKgB,
                    MetrosInicialB = model.MetrosInicialB,
                    MetrosFinalB = model.MetrosFinalB,
                    EspesorInicialCmB = model.EspesorInicialCmB,
                    EspesorFinalCmB = model.EspesorFinalCmB,
                    PesoInicialA = model.PesoInicialA,
                    CantidadUtilizadaA = model.CantidadUtilizadaA,
                    PesoFinalA = model.PesoFinalA,
                    VelocidadSuperiorA = model.VelocidadSuperiorA,
                    VelocidadInferiorA = model.VelocidadInferiorA,
                    LoteA = model.LoteA,
                    VencimientoA = model.VencimientoA,
                    PesoInicialB = model.PesoInicialB,
                    CantidadUtilizadaB = model.CantidadUtilizadaB,
                    PesoFinalB = model.PesoFinalB,
                    VelocidadSuperiorB = model.VelocidadSuperiorB,
                    VelocidadInferiorB = model.VelocidadInferiorB,
                    LoteB = model.LoteB,
                    VencimientoB = model.VencimientoB,
                    Observaciones = model.Observaciones,
                    CantidadArranques = model.CantidadArranques,
                    IdUsuarioActualizacion = userId
                };

                await _prdIlKwangService.UpdateAsync(dto);
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
                await PopulateDropdownListsAsync(model);
                return View(model);
            }
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> EditDetPrd(DetPrdIlKwangViewModel model)
        {
            if (model.detrpdId == null)
            {
                return Json(new { success = false, message = "Error: ID de detalle no proporcionado." });
            }

            try
            {
                var userId = _userManager.GetUserId(User);
                var dto = new DetPrdIlKwangDTO
                {
                    Id = model.detrpdId.Value,
                    IdEspesor = model.IdEspesor,
                    Cantidad = model.Cantidad,
                    Medida = model.Medida,
                    IdStatus = model.IdStatus,
                    IdTipo = model.IdTipo,
                    IdUsuarioActualizacion = userId
                };

                var res = await _prdIlKwangService.UpdateDetPrd(dto);
                return Json(new { success = res, message = "Actualización exitosa!" });
            }
            catch (KeyNotFoundException ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
            catch (Exception ex)
            {
                // Log the exception here if logging is available
                // _logger.LogError(ex, "Error updating DetPrdIlKwang");
                return Json(new { success = false, message = "Ocurrió un error inesperado durante la actualización." });
            }
        }

        // GET: PrdIlKwangController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrdIlKwangController/Delete/5
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
            var result = await _prdIlKwangService.ValidatePrdIlKwangByIdAsync(id, userId);
              
            return Json(result);
        }

        [HttpPost]
        [Authorize(Roles = "JefeProduccion")]
        public async Task<IActionResult> AprobarPrd(int id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _prdIlKwangService.AprovePrdIlKwangByIdAsync(id, userId);

            return Json(result);
        }

        [Authorize(Roles = "JefeProduccion")]
        public async Task<ActionResult> GetDataReport(DateTime start, DateTime end)
        {
            var reportData = await _prdIlKwangService.GetAllPrdIlKwangWithDetailsAsync(start, end);
            return View(reportData);
        }
    }
}
