﻿using Microsoft.AspNetCore.Mvc;

namespace ProyectoWeb_Sabado.Controllers
{
    public class ServicioController : Controller
    {
        [HttpGet]
        public IActionResult ConsultarServicios()
        {
            return View();
        }
    }
}
