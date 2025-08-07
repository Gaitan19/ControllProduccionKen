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

        public GestionCatalogosController( IGestionCatalogosService gestionCatalogosService)
        {
            _gestionCatalogosService = gestionCatalogosService;
        }

        public async Task<ActionResult> IndexAnchoBobina()
        {
            var dto = await _gestionCatalogosService.GetAllAnchoBobinaAsync();
            List<AnchoBobinaViewModel> anchoBobinas = new List<AnchoBobinaViewModel>(); 
            foreach(var d in dto)
            {
                AnchoBobinaViewModel ab = new AnchoBobinaViewModel();
                ab.Id = d.Id;
                ab.Valor = d.Valor;
                ab.Activo = (bool) d.Activo;
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
                await  _gestionCatalogosService.CreateAnchoBobinaAsync(dto);
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
        public ActionResult EditColoresBobina(int id)
        {
            var model = _gestionCatalogosService.GetByIdAsync(id).Result;
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
        public ActionResult EditColoresBobina(int id, ColoresBobinaViewModel model)
        {
            try
            {
                var dto = new ColoresBobinaDTO
                {
                    Id = model.Id,
                    Color = model.Color,
                    Ral = model.Ral,
                    Activo = model.Activo
                };

                 _gestionCatalogosService.UpdateAsync(dto);
                return RedirectToAction(nameof(IndexColorBobina));
            }
            catch
            {
                return View();
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

        public ActionResult IndexCatalogoCercha()
        {
            var dto = _gestionCatalogosService.GetAllCatalogoCerchaAsync().Result;
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
        public ActionResult EditCatalogoCercha(int id)
        {
            var model = _gestionCatalogosService.GetByCatalogoCerchaIdAsync(id).Result;
            var viewModel = new CatalogoCerchaViewModel
            {
                Id = model.Id,
                IdLineaProduccion = model.IdLineaProduccion,
                CodigoArticulo = model.CodigoArticulo,
                DescripcionArticulo = model.DescripcionArticulo,
                LongitudMetros = model.LongitudMetros,
                Activo = (bool) model.Activo
            };
            return View(viewModel);
        }

        // POST: GestionCatalogosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCatalogoCercha(int id, CatalogoCerchaViewModel model)
        {
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

                _gestionCatalogosService.UpdateCatalogoCerchaAsync(dto);
                return RedirectToAction(nameof(IndexCatalogoCercha));
            }
            catch
            {
                return View();
            }
        }

        // catalogoMallasCovintec

        public ActionResult IndexCatalogoMallasCovintec()
        {
            var dto = _gestionCatalogosService.GetAllCatalogoMallasCovintecAsync().Result;
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
                try {
                    var dto = new CatalogoMallasCovintecDTO
                    {
                        CodigoArticulo = model.CodigoArticulo,
                        DescripcionArticulo = model.DescripcionArticulo,
                        IdLineaProduccion = model.IdLineaProduccion,
                        LongitudCentimetros = model.LongitudCentimetros,
                        Activo = model.Activo
                    };
                  await  _gestionCatalogosService.CreateCatalogoMallasCovintecAsync(dto);
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

        public ActionResult EditCatalogoMallasCovintec(int id)
        {
            var model = _gestionCatalogosService.GetByCatalogoMallasCovintecIdAsync(id).Result;
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
        public ActionResult EditCatalogoMallasCovintec(int id, CatalogoMallasCovintecViewModel model)
        {
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

                _gestionCatalogosService.UpdateCatalogoMallasCovintecAsync(dto);
                return RedirectToAction(nameof(IndexCatalogoMallasCovintec));
            }
            catch
            {
                return View();
            }
        }

        // catalogoPanelesCovintec


        public IActionResult IndexCatalogoPanelesCovintec()
        {
            var dto = _gestionCatalogosService.GetAllCatalogoPanelesCovintecAsync().Result;
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
                try {
                    var dto = new CatalogoPanelesCovintecDTO
                    {
                        Id = model.Id,
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
        public ActionResult EditCatalogoPanelesCovintec(int id)
        {
            var model = _gestionCatalogosService.GetByCatalogoPanelesCovintecIdAsync(id).Result;
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
        public ActionResult EditCatalogoPanelesCovintec(int id, CatalogoPanelesCovintecViewModel model)
        {
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

                _gestionCatalogosService.UpdateCatalogoPanelesCovintecAsync(dto);
                return RedirectToAction(nameof(IndexCatalogoPanelesCovintec));
            }
            catch
            {
                return View();
            }
        }

        // catalogo status

        public IActionResult IndexCatalogoStatus()
        {
            var dto = _gestionCatalogosService.GetAllCatalogoStatusAsync().Result;
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
        public ActionResult CreateCatalogoStatus(CatalogoStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new CatalogoStatusDTO
                {
                    Id = model.Id,
                    Descripcion = model.Descripcion,
                    Activo = model.Activo
                };
                _gestionCatalogosService.CreateCatalogoStatusAsync(dto);
                return RedirectToAction(nameof(IndexCatalogoStatus));
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public ActionResult EditCatalogoStatus(int id)
        {
            var model = _gestionCatalogosService.GetByCatalogoStatusIdAsync(id).Result;
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
        public ActionResult EditCatalogoStatus(int id, CatalogoStatusViewModel model)
        {
            try
            {
                var dto = new CatalogoStatusDTO
                {
                    Id = model.Id,
                    Descripcion = model.Descripcion,
                    Activo = model.Activo
                };

                _gestionCatalogosService.UpdateCatalogoStatusAsync(dto);
                return RedirectToAction(nameof(IndexCatalogoStatus));
            }
            catch
            {
                return View();
            }
        }

        // catalogo tipo

        public IActionResult IndexCatalogoTipo()
        {
            var dto = _gestionCatalogosService.GetAllCatalogoTipoAsync().Result;
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
        public ActionResult CreateCatalogoTipo(CatalogoTipoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new CatalogoTipoDTO
                {
                    Id = model.Id,
                    Descripcion = model.Descripcion,
                    Activo = model.Activo
                };
                _gestionCatalogosService.CreateCatalogoTipoAsync(dto);
                return RedirectToAction(nameof(IndexCatalogoTipo));
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        
        public ActionResult EditCatalogoTipo(int id)
        {
            var model = _gestionCatalogosService.GetByCatalogoTipoIdAsync(id).Result;
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
        public ActionResult EditCatalogoTipo(int id, CatalogoTipoViewModel model)
        {
            try
            {
                var dto = new CatalogoTipoDTO
                {
                    Id = model.Id,
                    Descripcion = model.Descripcion,
                    Activo = model.Activo
                };

                _gestionCatalogosService.UpdateCatalogoTipoAsync(dto);
                return RedirectToAction(nameof(IndexCatalogoTipo));
            }
            catch
            {
                return View();
            }
        }

        // catalogo Linea Produccion

        public IActionResult IndexLineaProduccion()
        {
            var dto = _gestionCatalogosService.GetAllLineaProduccionAsync().Result;
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
        public ActionResult CreateLineaProduccion(LineaProduccionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new LineaProduccionDTO
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo
                };
                _gestionCatalogosService.CreateLineaProduccionAsync(dto);
                return RedirectToAction(nameof(IndexLineaProduccion));
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public ActionResult EditLineaProduccion(int id)
        {
            var model = _gestionCatalogosService.GetByLineaProduccionIdAsync(id).Result;
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
        public ActionResult EditLineaProduccion(int id, LineaProduccionViewModel model)
        {
            try
            {
                var dto = new LineaProduccionDTO
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo
                };

                _gestionCatalogosService.UpdateLineaProduccionAsync(dto);
                return RedirectToAction(nameof(IndexLineaProduccion));
            }
            catch
            {
                return View();
            }
        }

        // maquina

        public IActionResult IndexMaquina()
        {
            var dto = _gestionCatalogosService.GetAllMaquinaAsync().Result;
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
        public ActionResult CreateMaquina(MaquinaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new MaquinaDto
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo
                };
                _gestionCatalogosService.CreateMaquinaAsync(dto);
                return RedirectToAction(nameof(IndexMaquina));
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public ActionResult EditMaquina(int id)
        {
            var model = _gestionCatalogosService.GetByMaquinaIdAsync(id).Result;
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
        public ActionResult EditMaquina(int id, MaquinaViewModel model)
        {
            try
            {
                var dto = new MaquinaDto
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Activo = model.Activo
                };

                _gestionCatalogosService.UpdateMaquinaAsync(dto);
                return RedirectToAction(nameof(IndexMaquina));
            }
            catch
            {
                return View();
            }
        }

        // tipo fabricacion
        public IActionResult IndexTipoFabricacion()
        {
            var dto = _gestionCatalogosService.GetAllTipoFabricacionAsync().Result;
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
        public ActionResult CreateTipoFabricacion(TipoFabricacionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new TipoFabricacionDto
                {
                    Id = model.Id,
                    Descripcion = model.Descripcion,
                    Activo = model.Activo
                };
                _gestionCatalogosService.CreateTipoFabricacionAsync(dto);
                return RedirectToAction(nameof(IndexTipoFabricacion));
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public ActionResult EditTipoFabricacion(int id)
        {
            var model = _gestionCatalogosService.GetByTipoFabricacionIdAsync(id).Result;
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
        public ActionResult EditTipoFabricacion(int id, TipoFabricacionViewModel model)
        {
            try
            {
                var dto = new TipoFabricacionDto
                {
                    Id = model.Id,
                    Descripcion = model.Descripcion,
                    Activo = model.Activo
                };

                _gestionCatalogosService.UpdateTipoFabricacionAsync(dto);
                return RedirectToAction(nameof(IndexTipoFabricacion));
            }
            catch
            {
                return View();
            }
        }

        // ubicacion bobina

        public IActionResult IndexUbicacionBobina()
        {
            var dto = _gestionCatalogosService.GetAllUbicacionBobinaAsync().Result;
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
        public ActionResult CreateUbicacionBobina(UbicacionBobinaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new UbicacionBobinaDTO
                {
                    Id = model.Id,
                    Descripcion = model.Descripcion,
                    Activo = model.Activo
                };
                _gestionCatalogosService.CreateUbicacionBobinaAsync(dto);
                return RedirectToAction(nameof(IndexUbicacionBobina));
            }

            return View(model);
        }

        // GET: GestionCatalogosController/Edit/5
        public ActionResult EditUbicacionBobina(int id)
        {
            var model = _gestionCatalogosService.GetByUbicacionBobinaIdAsync(id).Result;
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
        public ActionResult EditUbicacionBobina(int id, UbicacionBobinaViewModel model)
        {
            try
            {
                var dto = new UbicacionBobinaDTO
                {
                    Id = model.Id,
                    Descripcion = model.Descripcion,
                    Activo = model.Activo
                };

                _gestionCatalogosService.UpdateUbicacionBobinaAsync(dto);
                return RedirectToAction(nameof(IndexUbicacionBobina));
            }
            catch
            {
                return View();
            }
        }


    }
}
