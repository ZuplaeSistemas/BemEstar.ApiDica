using System.Security.Claims;
using System.Text;
using BemEstar.Dica.Infra.Db;
using BemEstar.Dica.Infra.Repositories;
using BemEstar.Dica.Models;
using Microsoft.AspNet.Identity;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.X509.Extension;

amespace BemEstar.Dica.Services;

    public class AuthCService
    {
        private readonly AuthControllerRepository _repository;
        private readonly PasswordHasher _passwordHasher;

        public AuthService(AuthRepository _repository)
        {
            this._repository = repository;
            this._passwordHasher = new PasswordHasher();
        }

        public stirng Login(string email, string password)
        {
            User model = this._repository.GetUserByEmail(email);
            if(model.Id != 0)
            {
                //O usuário não é nulo, logo veio algo, o email existe
                //validar a senha
                PasswordVerificationResult result = PasswordHasher.VerifyHashedPassword(model.Password, password);
                if (result == PasswordVerificationResult.Success)
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

                    return token;
                }
            }
            else
            {
                return "Usuário ou senha inválido";
            }
        }
        
        public string Logout(int userId)
        {
            return "Logged out";
        }
    }