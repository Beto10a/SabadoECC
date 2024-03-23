namespace ProyectoApi_Sabado.Entities
{
    public class Respuesta
    {
        public Respuesta()
        {
            Codigo = "00";
            Mensaje = string.Empty;
            ConsecutivoGenerado = -1;
        }

        public string? Codigo { get; set; }
        public string? Mensaje { get; set; }
        public long ConsecutivoGenerado { get; set; }
    }
}
