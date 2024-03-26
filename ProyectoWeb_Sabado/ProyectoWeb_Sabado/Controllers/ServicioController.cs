using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using ProyectoWeb_Sabado.Entidades;
using ProyectoWeb_Sabado.Models;
using ProyectoWeb_Sabado.Services;

namespace ProyectoWeb_Sabado.Controllers
{
    public class ServicioController(IServicioModel _servicioModel, IHostEnvironment _environment) : Controller
    {
        [HttpGet]
        public IActionResult ConsultarServicios()
        {
            var resp = _servicioModel.ConsultarServicios(true);

            if (resp?.Codigo == "00")
            {
                return View(resp!.Datos);
            }
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
        public IActionResult RegistrarServicio(IFormFile Imagen, Servicio entidad)
        {
            string ext = Path.GetExtension(Path.GetFileName(Imagen.FileName));
            string folder = Path.Combine(_environment.ContentRootPath, "wwwroot\\images");

            if (ext.ToLower() != ".png")
            {
                ViewBag.MsjPantalla = "La imagen debe ser .png";
                return View();
            }

            entidad.Imagen = "/images/";
            var resp = _servicioModel.RegistrarServicio(entidad);

            if (resp?.Codigo == "00")
            {
                string archivo = Path.Combine(folder, resp?.ConsecutivoGenerado + ext);
                using (Stream fileStream = new FileStream(archivo, FileMode.Create))
                {
                    Imagen.CopyTo(fileStream);
                }

                return RedirectToAction("ConsultarServicios", "Servicio");
            }
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }


        [HttpGet]
        public IActionResult ActualizarServicio(long id)
        {
            var resp = _servicioModel.ConsultarServicio(id);

            if (resp?.Codigo == "00")
            {
                ViewBag.urlImagen = resp?.Dato?.Imagen;
                ViewBag.urlVideo = resp?.Dato?.Video;
                return View(resp!.Dato);
            }
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }

        [HttpPost]
        public IActionResult ActualizarServicio(IFormFile Imagen, Servicio entidad)
        {
            string ext = string.Empty;
            string folder = string.Empty;

            if (Imagen != null)
            { 
                ext = Path.GetExtension(Path.GetFileName(Imagen.FileName));
                folder = Path.Combine(_environment.ContentRootPath, "wwwroot\\images");

                if (ext.ToLower() != ".png")
                {
                    ViewBag.MsjPantalla = "La imagen debe ser .png";
                    return View();
                }
            }

            var resp = _servicioModel.ActualizarServicio(entidad);

            if (resp?.Codigo == "00")
            {
                if (Imagen != null)
                {
                    string archivo = Path.Combine(folder, entidad.IdServicio + ext);
                    using (Stream fileStream = new FileStream(archivo, FileMode.Create))
                    {
                        Imagen.CopyTo(fileStream);
                    }
                }

                return RedirectToAction("ConsultarServicios", "Servicio");
            }
            else
            {
                ViewBag.MsjPantalla = resp?.Mensaje;
                return View();
            }
        }
    }
}