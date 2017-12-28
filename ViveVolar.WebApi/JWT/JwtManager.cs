using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace ViveVolar.WebApi.JWT
{
    public class JwtManager
    {


        /// <summary>
        /// genera token valido con base a una llave simetrica base 64 almacenada en el web config 
        /// </summary>
        /// <param name="usuario">nombre del usuario autenticado</param>
        /// <param name="tiempoExpirar">tiempo de expiracion del token</param>
        /// <returns></returns>
        public static string GenerateToken(string usuario, int tiempoExpirar = 20)
        {
            string Secret = ConfigurationManager.AppSettings["Secret"].ToString();
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, usuario)
                        }),

                Expires = now.AddMinutes(Convert.ToInt32(tiempoExpirar)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                string Secret = ConfigurationManager.AppSettings["Secret"].ToString();
                var symmetricKey = Encoding.ASCII.GetBytes(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}