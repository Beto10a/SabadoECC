using ProyectoWeb_Sabado.Entidades;
using ProyectoWeb_Sabado.Services;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ProyectoWeb_Sabado.Models
{
    public class ServicioModel(HttpClient _httpClient, IConfiguration _configuration, IHttpContextAccessor _context ) : IServicioModel
    {
        public ServicioRespuesta? ConsultarServicios(bool MostrarTodos)
        {
            string url = _configuration.GetSection("settings:UrlWebApi").Value + "api/Servicio/ConsultarServicios?MostrarTodos=" + MostrarTodos;

            string token = _context.HttpContext?.Session.GetString("Token")!;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<ServicioRespuesta>().Result;

            return null;
        }

        public ServicioRespuesta? ConsultarServicio(long IdServicio)
        {
            string url = _configuration.GetSection("settings:UrlWebApi").Value + "api/Servicio/ConsultarServicio?IdServicio=" + IdServicio;

            string token = _context.HttpContext?.Session.GetString("Token")!;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<ServicioRespuesta>().Result;

            return null;
        }

        public Respuesta? RegistrarServicio(Servicio entidad)
        {
            string url = _configuration.GetSection("settings:UrlWebApi").Value + "api/Servicio/RegistrarServicio";

            string token = _context.HttpContext?.Session.GetString("Token")!;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            JsonContent body = JsonContent.Create(entidad);
            var resp = _httpClient.PostAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

        public Respuesta? ActualizarServicio(Servicio entidad)
        {
            string url = _configuration.GetSection("settings:UrlWebApi").Value + "api/Servicio/ActualizarServicio";

            string token = _context.HttpContext?.Session.GetString("Token")!;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            JsonContent body = JsonContent.Create(entidad);
            var resp = _httpClient.PutAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<Respuesta>().Result;

            return null;
        }

    }
}
