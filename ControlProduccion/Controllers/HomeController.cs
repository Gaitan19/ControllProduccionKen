using Application.Interfaces;
using ControlProduccion.Models;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ControlProduccion.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITipoFabricacionService _tipoFabricacionService;

        public HomeController(ILogger<HomeController> logger, ITipoFabricacionService tipoFabricacionService)
        {
            _logger = logger;
            _tipoFabricacionService = tipoFabricacionService;
        }

        public IActionResult Index()
        {

          
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
