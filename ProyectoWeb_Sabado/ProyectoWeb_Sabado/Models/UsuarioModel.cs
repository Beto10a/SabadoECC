using ProyectoWeb_Sabado.Entidades;
using ProyectoWeb_Sabado.Services;

namespace ProyectoWeb_Sabado.Models
{
    public class UsuarioModel(HttpClient _httpClient, IConfiguration _configuration) : IUsuarioModel
    {
        public Respuesta? RegistrarUsuario(Usuario entidad)
        {
            string url = _configuration.GetSection("Parametros:UrlWebApi").Value + "api/Usuario/RegistrarUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

    }
}
