using Microsoft.AspNetCore.Mvc;
using ProyectoApi_Sabado.Entidades;
using Microsoft.AspNetCore.Authorization;
using ProyectoApi_Sabado.Services;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using ProyectoApi_Sabado.Entities;

namespace ProyectoApi_Sabado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IConfiguration _configuration) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("IniciarSesion")]
        public IActionResult IniciarSesion(Usuario entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = db.Query<Usuario>("IniciarSesion",
                    new { entidad.Correo, entidad.Contrasenna },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                return Ok(resultado);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RegistrarUsuario")]
        public IActionResult RegistrarUsuario(Usuario entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                Respuesta respuesta = new Respuesta();

                var resultado = db.Execute("RegistrarUsuario", 
                    new { entidad.Correo, entidad.Contrasenna, entidad.NombreUsuario }, 
                    commandType: CommandType.StoredProcedure);

                if (resultado <= 0)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "Su correo ya se encuentra registrado";
                }

                return Ok(respuesta);

            }                
        }

    }
}
