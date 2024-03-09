using ProyectoWeb_Sabado.Entidades;

namespace ProyectoWeb_Sabado.Services
{
    public interface IUsuarioModel
    {
        Respuesta? RegistrarUsuario(Usuario entidad);

        UsuarioRespuesta? IniciarSesion(Usuario entidad);

        UsuarioRespuesta? RecuperarAcceso(Usuario entidad);

        UsuarioRespuesta? CambiarContrasenna(Usuario entidad);
    }
}
