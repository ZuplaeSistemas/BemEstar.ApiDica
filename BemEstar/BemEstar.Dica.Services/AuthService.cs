using System.Security.Claims;
using System.Text;
using BemEstar.Dica.Infra.Db;
using BemEstar.Dica.Infra.Repositories;
using BemEstar.Dica.Models;
using Microsoft.AspNet.Identity;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.X509.Extension;

namespace BemEstar.Dica.Services;

    public class AuthCService
    {
        private readonly AuthControllerRepository _repository;
        private readonly AuthRepository _jwtTokenService;
        private readonly PasswordHasher _passwordHasher;

        public AuthService(AuthRepository repository, JwtTokenService jwtTokenService)
        {
            this._repository = repository;
            this.jwtTokenService = jwtTokenService;
            this._passwordHasher = new PasswordHasher();
        }

        //Precisa ter responsabilidade única e separar o login
        public string Login(string email, string password)
        {
            User model = this._repository.GetUserByEmail(email);
            if(model.Id != 0)
            {
                //O usuário não é nulo, logo veio algo, o email existe
                //validar a senha
                PasswordVerificationResult result = PasswordHasher.VerifyHashedPassword(model.Password, password);
                if (result == PasswordVerificationResult.Success)
                {
                    return jwtTokenService.GenerateToken(model);

                }
            }
            throw new Exception("Usuário ou senha inválido");
        }
        
        public string Logout(int userId)
        {
            return "Logged out";
        }
    }