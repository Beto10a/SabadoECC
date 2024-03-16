using ProyectoWeb_Sabado.Entidades;

namespace ProyectoWeb_Sabado.Services
{
    public interface IServicioModel
    {
        ServicioRespuesta? ConsultarServicios(bool MostrarTodos);
        Respuesta? RegistrarServicio(Servicio entidad);
    }
}
