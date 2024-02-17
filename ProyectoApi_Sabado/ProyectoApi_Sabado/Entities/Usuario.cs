namespace ProyectoApi_Sabado.Entidades
{
    public class Usuario
    {
        public long IdUsuario { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string Contrasenna { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public short IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}