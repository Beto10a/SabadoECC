using Microsoft.AspNetCore.Mvc;
using ProyectoApi_Sabado.Entidades;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoApi_Sabado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        [AllowAnonymous]
        [HttpPost]
        [Route("IniciarSesion")]
        public IActionResult ConsultarUsuarios(Usuario entidad)
        {
            if (entidad.cedula == "304590415" && entidad.contrasenna == "secreta")
                return Ok(GenerarToken(entidad.cedula));

            return NotFound("Sus credenciales no son correctas");
        }

        [Authorize]
        [HttpGet]
        [Route("ConsultarDia")]
        public IActionResult ConsultarDia()
        {
            var claims = User.Claims;

            return Ok(DateTime.Now);
        }

        private string GenerarToken(string cedula)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("username", cedula));
            claims.Add(new Claim("userrol", "ADMIN"));

            string SecretKey = "erQuPVWaBcnFePyQEGRhDjFCzbtGBLgL";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
