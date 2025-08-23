using Application.DTOs;
using Application.Interfaces;
using ControlProduccion.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControlProduccion.Controllers
{
    public class GestionCatalogosController : Controller
    {
        private IGestionCatalogosService _gestionCatalogosService;

        public GestionCatalogosController(IGestionCatalogosService gestionCatalogosService)
        {
            _gestionCatalogosService = gestionCatalogosService;
        }

        public async Task<ActionResult> IndexAnchoBobina()
        {
            var dto = await _gestionCatalogosService.GetAllAnchoBobinaAsync();
            List<AnchoBobinaViewModel> anchoBobinas = new List<AnchoBobinaViewModel>();
            foreach (var d in dto)
            {
                AnchoBobinaViewModel ab = new AnchoBobinaViewModel();
                ab.Id = d.Id;
                ab.Valor = d.Valor;
                ab.Activo = (bool)d.Activo;
                anchoBobinas.Add(ab);
            }

            return View(anchoBobinas);
        }

        // GET: GestionCatalogosController
        public async Task<ActionResult> IndexColorBobina()
        {
            var model = await _gestionCatalogosService.GetAllColoresBobinaAsync();
            List<ColoresBobinaViewModel> coloresBobinaViewModels = new List<ColoresBobinaViewModel>();
            foreach (var item in model)
            {
                var viewModel = new ColoresBobinaViewModel
                {
                    Id = item.Id,
                    Color = item.Color,
                    Ral = item.Ral,
                    Activo = (bool)item.Activo
                };

                coloresBobinaViewModels.Add(viewModel);
            }

            return View(coloresBobinaViewModels);
        }

        // GET: GestionCatalogosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult CreateAnchoBobina()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAnchoBobina(AnchoBobinaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new AnchoBobinaDTO
                {
                    Valor = model.Valor,
                    Activo = (bool)model.Activo
                };
                await _gestionCatalogosService.CreateAnchoBobinaAsync(dto);
                return RedirectToAction(nameof(IndexAnchoBobina));
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Create
        public ActionResult CreateColorBobina()
        {
            return View();
        }

        // POST: GestionCatalogosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateColorBonina(ColoresBobinaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new ColoresBobinaDTO
                {
                    Color = model.Color,
                    Ral = model.Ral,
                    Activo = model.Activo
                };
                await _gestionCatalogosService.CreateAsync(dto);
                return RedirectToAction(nameof(IndexColorBobina));
            }

            return View(model);
        }

        public async Task<ActionResult> EditAnchoBobina(int id)
        {
            var model = await _gestionCatalogosService.GetByAnchoBobinaIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new AnchoBobinaViewModel
            {
                Id = model.Id,
                Valor = model.Valor,
                Activo = (bool)model.Activo
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAnchoBobina(int id, AnchoBobinaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new AnchoBobinaDTO
                {
                    Id = model.Id,
                    Valor = model.Valor,
                    Activo = model.Activo
                };

                await _gestionCatalogosService.UpdateAAnchoBobinasync(dto);
                return RedirectToAction(nameof(IndexAnchoBobina));
            }
            catch (Exception ex)
            {
                // Log the exception if logging is available
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro.");
                return View(model);
            }
        }

        // GET: GestionCatalogosController/Edit/5
        public async Task<ActionResult> EditColoresBobina(int id)
        {
            var model = await _gestionCatalogosService.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new ColoresBobinaViewModel
            {
                Id = model.Id,
                Color = model.Color,
                Ral = model.Ral,
                Activo = (bool)model.Activo
            };
            return View(viewModel);
        }

        // POST: GestionCatalogosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditColoresBobina(int id, ColoresBobinaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new ColoresBobinaDTO
                {
                    Id = model.Id,
                    Color = model.Color,
                    Ral = model.Ral,
                    Activo = model.Activo
                };

                await _gestionCatalogosService.UpdateAsync(dto);
                return RedirectToAction(nameof(IndexColorBobina));
            }
            catch (Exception ex)
            {
                // Log the exception if logging is available
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro.");
                return View(model);
            }
        }

        // GET: GestionCatalogosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GestionCatalogosController/Delete/5
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

        // CatalogoCercha
        public async Task<ActionResult> IndexCatalogoCercha()
        {
            var dto = await _gestionCatalogosService.GetAllCatalogoCerchaAsync();
            List<CatalogoCerchaViewModel> catalogoCerchas = new List<CatalogoCerchaViewModel>();
            foreach (var d in dto)
            {
                CatalogoCerchaViewModel ab = new CatalogoCerchaViewModel();
                ab.Id = d.Id;
                ab.IdLineaProduccion = d.IdLineaProduccion;
                ab.CodigoArticulo = d.CodigoArticulo;
                ab.DescripcionArticulo = d.DescripcionArticulo;
                ab.LongitudMetros = d.LongitudMetros;
                ab.Activo = (bool)d.Activo;

                catalogoCerchas.Add(ab);
            }

            return View(catalogoCerchas);
        }

        // GET: GestionCatalogosController/Create
        public ActionResult CreateCatalogoCercha()
        {
            return View();
        }

        // POST: GestionCatalogosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCatalogoCercha(CatalogoCerchaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new CatalogoCerchaDTO
                    {
                        CodigoArticulo = model.CodigoArticulo,
                        DescripcionArticulo = model.DescripcionArticulo,
                        IdLineaProduccion = model.IdLineaProduccion,
                        LongitudMetros = model.LongitudMetros,
                        Activo = model.Activo
                    };

                    await _gestionCatalogosService.CreateCatalogoCerchaAsync(dto);

                    return RedirectToAction(nameof(IndexCatalogoCercha));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", "Error al crear el catálogo de cerchas: " + ex.Message);
                }
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public async Task<ActionResult> EditCatalogoCercha(int id)
        {
            var model = await _gestionCatalogosService.GetByCatalogoCerchaIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new CatalogoCerchaViewModel
            {
                Id = model.Id,
                IdLineaProduccion = model.IdLineaProduccion,
                CodigoArticulo = model.CodigoArticulo,
                DescripcionArticulo = model.DescripcionArticulo,
                LongitudMetros = model.LongitudMetros,
                Activo = (bool)model.Activo
            };
            return View(viewModel);
        }

        // POST: GestionCatalogosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCatalogoCercha(int id, CatalogoCerchaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new CatalogoCerchaDTO
                {
                    Id = model.Id,
                    IdLineaProduccion = model.IdLineaProduccion,
                    CodigoArticulo = model.CodigoArticulo,
                    DescripcionArticulo = model.DescripcionArticulo,
                    LongitudMetros = model.LongitudMetros,
                    Activo = model.Activo
                };

                await _gestionCatalogosService.UpdateCatalogoCerchaAsync(dto);
                return RedirectToAction(nameof(IndexCatalogoCercha));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro.");
                return View(model);
            }
        }

        // catalogoMallasCovintec
        public async Task<ActionResult> IndexCatalogoMallasCovintec()
        {
            var dto = await _gestionCatalogosService.GetAllCatalogoMallasCovintecAsync();
            List<CatalogoMallasCovintecViewModel> catalogoMallasCovintecs = new List<CatalogoMallasCovintecViewModel>();
            foreach (var d in dto)
            {
                CatalogoMallasCovintecViewModel ab = new CatalogoMallasCovintecViewModel();
                ab.Id = d.Id;
                ab.IdLineaProduccion = d.IdLineaProduccion;
                ab.CodigoArticulo = d.CodigoArticulo;
                ab.DescripcionArticulo = d.DescripcionArticulo;
                ab.LongitudCentimetros = d.LongitudCentimetros;
                ab.Activo = (bool)d.Activo;

                catalogoMallasCovintecs.Add(ab);
            }

            return View(catalogoMallasCovintecs);
        }

        // GET: GestionCatalogosController/Create
        public ActionResult CreateCatalogoMallasCovintec()
        {
            return View();
        }

        // POST: GestionCatalogosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCatalogoMallasCovintec(CatalogoMallasCovintecViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new CatalogoMallasCovintecDTO
                    {
                        CodigoArticulo = model.CodigoArticulo,
                        DescripcionArticulo = model.DescripcionArticulo,
                        IdLineaProduccion = model.IdLineaProduccion,
                        LongitudCentimetros = model.LongitudCentimetros,
                        Activo = model.Activo
                    };
                    await _gestionCatalogosService.CreateCatalogoMallasCovintecAsync(dto);
                    return RedirectToAction(nameof(IndexCatalogoMallasCovintec));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", "Error al crear el catálogo de mallas: " + ex.Message);
                }
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public async Task<ActionResult> EditCatalogoMallasCovintec(int id)
        {
            var model = await _gestionCatalogosService.GetByCatalogoMallasCovintecIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new CatalogoMallasCovintecViewModel
            {
                Id = model.Id,
                IdLineaProduccion = model.IdLineaProduccion,
                CodigoArticulo = model.CodigoArticulo,
                DescripcionArticulo = model.DescripcionArticulo,
                LongitudCentimetros = model.LongitudCentimetros,
                Activo = (bool)model.Activo
            };
            return View(viewModel);
        }

        // POST: GestionCatalogosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCatalogoMallasCovintec(int id, CatalogoMallasCovintecViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new CatalogoMallasCovintecDTO
                {
                    Id = model.Id,
                    IdLineaProduccion = model.IdLineaProduccion,
                    CodigoArticulo = model.CodigoArticulo,
                    DescripcionArticulo = model.DescripcionArticulo,
                    LongitudCentimetros = model.LongitudCentimetros,
                    Activo = model.Activo
                };

                await _gestionCatalogosService.UpdateCatalogoMallasCovintecAsync(dto);
                return RedirectToAction(nameof(IndexCatalogoMallasCovintec));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro.");
                return View(model);
            }
        }

        // catalogoPanelesCovintec
        public async Task<IActionResult> IndexCatalogoPanelesCovintec()
        {
            var dto = await _gestionCatalogosService.GetAllCatalogoPanelesCovintecAsync();
            List<CatalogoPanelesCovintecViewModel> catalogoPanelesCovintecs = new List<CatalogoPanelesCovintecViewModel>();
            foreach (var d in dto)
            {
                CatalogoPanelesCovintecViewModel ab = new CatalogoPanelesCovintecViewModel();
                ab.Id = d.Id;
                ab.IdLineaProduccion = d.IdLineaProduccion;
                ab.CodigoArticulo = d.CodigoArticulo;
                ab.DescripcionArticulo = d.DescripcionArticulo;
                ab.Mts2PorPanel = d.Mts2PorPanel;
                ab.Activo = (bool)d.Activo;

                catalogoPanelesCovintecs.Add(ab);
            }

            return View(catalogoPanelesCovintecs);
        }

        // GET: GestionCatalogosController/Create
        public ActionResult CreateCatalogoPanelesCovintec()
        {
            return View();
        }

        // POST: GestionCatalogosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCatalogoPanelesCovintec(CatalogoPanelesCovintecViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new CatalogoPanelesCovintecDTO
                    {
                        CodigoArticulo = model.CodigoArticulo,
                        DescripcionArticulo = model.DescripcionArticulo,
                        IdLineaProduccion = model.IdLineaProduccion,
                        Mts2PorPanel = model.Mts2PorPanel,
                        Activo = model.Activo
                    };
                    await _gestionCatalogosService.CreateCatalogoPanelesCovintecAsync(dto);

                    return RedirectToAction(nameof(IndexCatalogoPanelesCovintec));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", "Error al crear el catálogo de paneles: " + ex.Message);
                }
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public async Task<ActionResult> EditCatalogoPanelesCovintec(int id)
        {
            var model = await _gestionCatalogosService.GetByCatalogoPanelesCovintecIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new CatalogoPanelesCovintecViewModel
            {
                Id = model.Id,
                IdLineaProduccion = model.IdLineaProduccion,
                CodigoArticulo = model.CodigoArticulo,
                DescripcionArticulo = model.DescripcionArticulo,
                Mts2PorPanel = model.Mts2PorPanel,
                Activo = (bool)model.Activo
            };
            return View(viewModel);
        }

        // POST: GestionCatalogosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCatalogoPanelesCovintec(int id, CatalogoPanelesCovintecViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new CatalogoPanelesCovintecDTO
                {
                    Id = model.Id,
                    IdLineaProduccion = model.IdLineaProduccion,
                    CodigoArticulo = model.CodigoArticulo,
                    DescripcionArticulo = model.DescripcionArticulo,
                    Mts2PorPanel = model.Mts2PorPanel,
                    Activo = model.Activo
                };

                await _gestionCatalogosService.UpdateCatalogoPanelesCovintecAsync(dto);
                return RedirectToAction(nameof(IndexCatalogoPanelesCovintec));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro.");
                return View(model);
            }
        }

        // catalogo status
        public async Task<IActionResult> IndexCatalogoStatus()
        {
            var dto = await _gestionCatalogosService.GetAllCatalogoStatusAsync();
            List<CatalogoStatusViewModel> catalogoStatus = new List<CatalogoStatusViewModel>();
            foreach (var d in dto)
            {
                CatalogoStatusViewModel ab = new CatalogoStatusViewModel();
                ab.Id = d.Id;
                ab.Descripcion = d.Descripcion;
                ab.Activo = (bool)d.Activo;

                catalogoStatus.Add(ab);
            }

            return View(catalogoStatus);
        }

        // GET: GestionCatalogosController/Create
        public ActionResult CreateCatalogoStatus()
        {
            return View();
        }

        // POST: GestionCatalogosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCatalogoStatus(CatalogoStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new CatalogoStatusDTO
                    {
                        Id = model.Id,
                        Descripcion = model.Descripcion,
                        Activo = model.Activo
                    };
                    await _gestionCatalogosService.CreateCatalogoStatusAsync(dto);
                    return RedirectToAction(nameof(IndexCatalogoStatus));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear el registro.");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public async Task<ActionResult> EditCatalogoStatus(int id)
        {
            var model = await _gestionCatalogosService.GetByCatalogoStatusIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new CatalogoStatusViewModel
            {
                Id = model.Id,
                Descripcion = model.Descripcion,
                Activo = (bool)model.Activo
            };
            return View(viewModel);
        }

        // POST: GestionCatalogosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCatalogoStatus(int id, CatalogoStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new CatalogoStatusDTO
                {
                    Id = model.Id,
                    Descripcion = model.Descripcion,
                    Activo = model.Activo
                };

                await _gestionCatalogosService.UpdateCatalogoStatusAsync(dto);
                return RedirectToAction(nameof(IndexCatalogoStatus));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro.");
                return View(model);
            }
        }

        // catalogo tipo
        public async Task<IActionResult> IndexCatalogoTipo()
        {
            var dto = await _gestionCatalogosService.GetAllCatalogoTipoAsync();
            List<CatalogoTipoViewModel> catalogoTipos = new List<CatalogoTipoViewModel>();
            foreach (var d in dto)
            {
                CatalogoTipoViewModel ab = new CatalogoTipoViewModel();
                ab.Id = d.Id;
                ab.Descripcion = d.Descripcion;
                ab.Activo = (bool)d.Activo;

                catalogoTipos.Add(ab);
            }

            return View(catalogoTipos);
        }

        // GET: GestionCatalogosController/Create
        public ActionResult CreateCatalogoTipo()
        {
            return View();
        }

        // POST: GestionCatalogosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCatalogoTipo(CatalogoTipoViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new CatalogoTipoDTO
                    {
                        Id = model.Id,
                        Descripcion = model.Descripcion,
                        Activo = model.Activo
                    };
                    await _gestionCatalogosService.CreateCatalogoTipoAsync(dto);
                    return RedirectToAction(nameof(IndexCatalogoTipo));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear el registro.");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public async Task<ActionResult> EditCatalogoTipo(int id)
        {
            var model = await _gestionCatalogosService.GetByCatalogoTipoIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new CatalogoTipoViewModel
            {
                Id = model.Id,
                Descripcion = model.Descripcion,
                Activo = (bool)model.Activo
            };
            return View(viewModel);
        }

        // POST: GestionCatalogosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCatalogoTipo(int id, CatalogoTipoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new CatalogoTipoDTO
                {
                    Id = model.Id,
                    Descripcion = model.Descripcion,
                    Activo = model.Activo
                };

                await _gestionCatalogosService.UpdateCatalogoTipoAsync(dto);
                return RedirectToAction(nameof(IndexCatalogoTipo));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro.");
                return View(model);
            }
        }

        // catalogo Linea Produccion
        public async Task<IActionResult> IndexLineaProduccion()
        {
            var dto = await _gestionCatalogosService.GetAllLineaProduccionAsync();
            List<LineaProduccionViewModel> lineaProduccions = new List<LineaProduccionViewModel>();
            foreach (var d in dto)
            {
                LineaProduccionViewModel ab = new LineaProduccionViewModel();
                ab.Id = d.Id;
                ab.Nombre = d.Nombre;
                ab.Activo = (bool)d.Activo;

                lineaProduccions.Add(ab);
            }

            return View(lineaProduccions);
        }

        // GET: GestionCatalogosController/Create
        public ActionResult CreateLineaProduccion()
        {
            return View();
        }

        // POST: GestionCatalogosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateLineaProduccion(LineaProduccionViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new LineaProduccionDTO
                    {
                        Id = model.Id,
                        Nombre = model.Nombre,
                        Activo = model.Activo
                    };
                    await _gestionCatalogosService.CreateLineaProduccionAsync(dto);
                    return RedirectToAction(nameof(IndexLineaProduccion));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear el registro.");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public async Task<ActionResult> EditLineaProduccion(int id)
        {
            var model = await _gestionCatalogosService.GetByLineaProduccionIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new LineaProduccionViewModel
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Activo = (bool)model.Activo
            };
            return View(viewModel);
        }

        // POST: GestionCatalogosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLineaProduccion(int id, LineaProduccionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new LineaProduccionDTO
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo
                };

                await _gestionCatalogosService.UpdateLineaProduccionAsync(dto);
                return RedirectToAction(nameof(IndexLineaProduccion));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro.");
                return View(model);
            }
        }

        // maquina
        public async Task<IActionResult> IndexMaquina()
        {
            var dto = await _gestionCatalogosService.GetAllMaquinaAsync();
            List<MaquinaViewModel> maquinas = new List<MaquinaViewModel>();
            foreach (var d in dto)
            {
                MaquinaViewModel ab = new MaquinaViewModel();
                ab.Id = d.Id;
                ab.Nombre = d.Nombre;
                ab.Activo = (bool)d.Activo;

                maquinas.Add(ab);
            }

            return View(maquinas);
        }

        // GET: GestionCatalogosController/Create
        public ActionResult CreateMaquina()
        {
            return View();
        }

        // POST: GestionCatalogosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateMaquina(MaquinaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new MaquinaDto
                    {
                        Id = model.Id,
                        Nombre = model.Nombre,
                        Activo = model.Activo
                    };
                    await _gestionCatalogosService.CreateMaquinaAsync(dto);
                    return RedirectToAction(nameof(IndexMaquina));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear el registro.");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public async Task<ActionResult> EditMaquina(int id)
        {
            var model = await _gestionCatalogosService.GetByMaquinaIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new MaquinaViewModel
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Activo = (bool)model.Activo
            };
            return View(viewModel);
        }

        // POST: GestionCatalogosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMaquina(int id, MaquinaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new MaquinaDto
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo
                };

                await _gestionCatalogosService.UpdateMaquinaAsync(dto);
                return RedirectToAction(nameof(IndexMaquina));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro.");
                return View(model);
            }
        }

        // tipo fabricacion
        public async Task<IActionResult> IndexTipoFabricacion()
        {
            var dto = await _gestionCatalogosService.GetAllTipoFabricacionAsync();
            List<TipoFabricacionViewModel> tipoFabricacions = new List<TipoFabricacionViewModel>();
            foreach (var d in dto)
            {
                TipoFabricacionViewModel ab = new TipoFabricacionViewModel();
                ab.Id = d.Id;
                ab.Descripcion = d.Descripcion;
                ab.Activo = (bool)d.Activo;

                tipoFabricacions.Add(ab);
            }

            return View(tipoFabricacions);
        }

        // GET: GestionCatalogosController/Create
        public ActionResult CreateTipoFabricacion()
        {
            return View();
        }

        // POST: GestionCatalogosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTipoFabricacion(TipoFabricacionViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new TipoFabricacionDto
                    {
                        Id = model.Id,
                        Descripcion = model.Descripcion,
                        Activo = model.Activo
                    };
                    await _gestionCatalogosService.CreateTipoFabricacionAsync(dto);
                    return RedirectToAction(nameof(IndexTipoFabricacion));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear el registro.");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public async Task<ActionResult> EditTipoFabricacion(int id)
        {
            var model = await _gestionCatalogosService.GetByTipoFabricacionIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new TipoFabricacionViewModel
            {
                Id = model.Id,
                Descripcion = model.Descripcion,
                Activo = (bool)model.Activo
            };
            return View(viewModel);
        }

        // POST: GestionCatalogosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTipoFabricacion(int id, TipoFabricacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new TipoFabricacionDto
                {
                    Id = model.Id,
                    Descripcion = model.Descripcion,
                    Activo = model.Activo
                };

                await _gestionCatalogosService.UpdateTipoFabricacionAsync(dto);
                return RedirectToAction(nameof(IndexTipoFabricacion));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro.");
                return View(model);
            }
        }

        // ubicacion bobina
        public async Task<IActionResult> IndexUbicacionBobina()
        {
            var dto = await _gestionCatalogosService.GetAllUbicacionBobinaAsync();
            List<UbicacionBobinaViewModel> ubicacionBobinas = new List<UbicacionBobinaViewModel>();
            foreach (var d in dto)
            {
                UbicacionBobinaViewModel ab = new UbicacionBobinaViewModel();
                ab.Id = d.Id;
                ab.Descripcion = d.Descripcion;
                ab.Activo = (bool)d.Activo;

                ubicacionBobinas.Add(ab);
            }

            return View(ubicacionBobinas);
        }

        // GET: GestionCatalogosController/Create
        public ActionResult CreateUbicacionBobina()
        {
            return View();
        }

        // POST: GestionCatalogosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUbicacionBobina(UbicacionBobinaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new UbicacionBobinaDTO
                    {
                        Id = model.Id,
                        Descripcion = model.Descripcion,
                        Activo = model.Activo
                    };
                    await _gestionCatalogosService.CreateUbicacionBobinaAsync(dto);
                    return RedirectToAction(nameof(IndexUbicacionBobina));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al crear el registro.");
                    return View(model);
                }
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public async Task<ActionResult> EditUbicacionBobina(int id)
        {
            var model = await _gestionCatalogosService.GetByUbicacionBobinaIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            var viewModel = new UbicacionBobinaViewModel
            {
                Id = model.Id,
                Descripcion = model.Descripcion,
                Activo = (bool)model.Activo
            };
            return View(viewModel);
        }

        // POST: GestionCatalogosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUbicacionBobina(int id, UbicacionBobinaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var dto = new UbicacionBobinaDTO
                {
                    Id = model.Id,
                    Descripcion = model.Descripcion,
                    Activo = model.Activo
                };

                await _gestionCatalogosService.UpdateUbicacionBobinaAsync(dto);
                return RedirectToAction(nameof(IndexUbicacionBobina));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al actualizar el registro.");
                return View(model);
            }
        }
    }
}