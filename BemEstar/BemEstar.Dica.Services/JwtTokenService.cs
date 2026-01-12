using System.Security.Claims;
using System.Text;
using BemEstar.Dica.Infra.Db;
using BemEstar.Dica.Infra.Repositories;
using BemEstar.Dica.Models;
using Microsoft.AspNet.Identity;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.X509.Extension;

namespace BemEstar.Dica.Services;

    public class JwtTokenService
    {
        public JwtTokenService(IOptions<JwtOptions>)
        {

        }

        public string GenerateToken()
        {
            //Gerar uma chave de acesso criptografada garantindo que o usuário autenticou com sucesso
                    //JWT - JSON Web Token
                    //Claims

                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim("UserId", model.Id.ToString()),
                        new Claim("PersonId", model.Person_Id.ToString(),
                        new Claim(ClaimTypes.Role, "Admin")
                    };

                    var key = new SymetricSecurityKey(Encoding.UTF8.GetBytes("ZuplaeKey2026"));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var expiration = DateTime.Now.AddHours(2);

                    var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                        Issuer: "Dica",
                        Audience: "DicaApiClient",
                        claims: claims,
                        Expires: expiration,
                        signingCredentials: creds
                    );
                    
                    var tokenHandler = new JwtSecurityTokenHandler().WriteToken(token);

                    return tokenHandler;

        }
    }