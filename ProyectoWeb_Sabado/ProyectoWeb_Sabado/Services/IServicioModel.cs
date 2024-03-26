using ProyectoWeb_Sabado.Entidades;

namespace ProyectoWeb_Sabado.Services
{
    public interface IServicioModel
    {
        ServicioRespuesta? ConsultarServicios(bool MostrarTodos);
        ServicioRespuesta? ConsultarServicio(long IdServicio);
        Respuesta? RegistrarServicio(Servicio entidad);
        Respuesta? ActualizarServicio(Servicio entidad);
    }
}
