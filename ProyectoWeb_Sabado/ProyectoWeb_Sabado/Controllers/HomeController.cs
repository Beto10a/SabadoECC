using Microsoft.AspNetCore.Mvc;
using ProyectoWeb_Sabado.Entidades;
using ProyectoWeb_Sabado.Models;
using ProyectoWeb_Sabado.Services;

namespace ProyectoWeb_Sabado.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class HomeController : Controller
    {
        private readonly IUsuarioModel _usuarioModel;
        public HomeController(IUsuarioModel usuarioModel)
        {
            _usuarioModel = usuarioModel;
        }

        [Seguridad]
        public IActionResult PantallaInicio()
        {
            return View();
        }

        public IActionResult IniciarSesion()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario entidad)
        {
            var resp = _usuarioModel.RegistrarUsuario(entidad);

            if (resp > 0)
                return RedirectToAction("IniciarSesion", "Home");

            return View();
        }

    }
}
