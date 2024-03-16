using Microsoft.AspNetCore.Mvc;
using ProyectoWeb_Sabado.Entidades;
using ProyectoWeb_Sabado.Services;

namespace ProyectoWeb_Sabado.Controllers
{
    public class ServicioController(IServicioModel _servicioModel) : Controller
    {
        [HttpGet]
        public IActionResult ConsultarServicios()
        {
            var resp = _servicioModel.ConsultarServicios(true);

            if (resp?.Codigo == "00")
                return View(resp.Datos);
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View(new List<Servicio>());
            }
        }

        [HttpGet]
        public IActionResult RegistrarServicio()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarServicio(Servicio entidad)
        {
            return View();
        }

    }
}