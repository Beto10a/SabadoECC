using Microsoft.AspNetCore.Mvc;

namespace ProyectoWeb_Sabado.Controllers
{
    public class ServiciosController : Controller
    {
        [HttpGet]
        public IActionResult VerCompras()
        {
            return View();
        }
    }
}
