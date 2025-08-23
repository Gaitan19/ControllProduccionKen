using Microsoft.AspNetCore.Mvc;

namespace ControlProduccion.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("/boom2")]
        public IActionResult Boom2() => throw new Exception("Boom2 desde controlador");
    }
}
