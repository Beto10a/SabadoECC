using Microsoft.IdentityModel.Tokens;
using ProyectoApi_Sabado.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoApi_Sabado.Models
{
    public class UtilitariosModel(IConfiguration _configuration) : IUtilitariosModel
    {

        public string GenerarToken(string correo)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("username", correo));

            string SecretKey = _configuration.GetSection("settings:SecretKey").Value ?? string.Empty;
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
