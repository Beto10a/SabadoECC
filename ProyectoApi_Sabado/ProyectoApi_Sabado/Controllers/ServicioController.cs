using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoApi_Sabado.Entities;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoApi_Sabado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController(IConfiguration _configuration) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("ConsultarServicios")]
        public IActionResult ConsultarServicios(bool MostrarTodos)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                ServicioRespuesta respuesta = new ServicioRespuesta();

                var resultado = db.Query<Servicio>("ConsultarServicios",
                    new { MostrarTodos },
                    commandType: CommandType.StoredProcedure).ToList();

                if (resultado == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "No hay servicios registrados";
                }
                else
                {
                    respuesta.Datos = resultado;
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("ConsultarServicio")]
        public IActionResult ConsultarServicio(long IdServicio)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                ServicioRespuesta respuesta = new ServicioRespuesta();

                var resultado = db.Query<Servicio>("ConsultarServicio",
                    new { IdServicio },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (resultado == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "No hay servicios registrados";
                }
                else
                {
                    respuesta.Dato = resultado;
                }

                return Ok(respuesta);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("RegistrarServicio")]
        public IActionResult RegistrarServicio(Servicio entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                Respuesta respuesta = new Respuesta();

                var resultado = db.Query<Servicio>("RegistrarServicio",
                    new { entidad.Nombre, entidad.Precio, entidad.Imagen, entidad.Video },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (resultado == null)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "Este servicio ya se encuentra registrado";
                }
                else
                {
                    respuesta.ConsecutivoGenerado = resultado.IdServicio;
                }

                return Ok(respuesta);

            }
        }

        [Authorize]
        [HttpPut]
        [Route("ActualizarServicio")]
        public IActionResult ActualizarServicio(Servicio entidad)
        {
            using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                Respuesta respuesta = new Respuesta();

                var resultado = db.Execute("ActualizarServicio",
                    new { entidad.IdServicio, entidad.Precio, entidad.Video },
                    commandType: CommandType.StoredProcedure);

                if (resultado <= 0)
                {
                    respuesta.Codigo = "-1";
                    respuesta.Mensaje = "No se pudo actualizar este servicio";
                }

                return Ok(respuesta);
            }
        }
    }
}
