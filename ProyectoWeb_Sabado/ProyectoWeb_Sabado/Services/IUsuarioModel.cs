using ProyectoWeb_Sabado.Entidades;

namespace ProyectoWeb_Sabado.Services
{
    public interface IUsuarioModel
    {
        public Respuesta? RegistrarUsuario(Usuario entidad);

        public UsuarioRespuesta? IniciarSesion(Usuario entidad);
    }
}
