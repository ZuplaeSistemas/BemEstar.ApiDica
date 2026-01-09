using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Mvc;
namespace BemEstar.Dica.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //Criar os endpoints de login e logout

        private readonly AuthService service;

        public AuthController(AuthService service)
        {
            this._service = service;
        }
        [HttpPost("Login")] //Método Post é para informações sensíveis
        public string Login(string email, string password)
        {
            string retorno = this._service.Login(email, password);
            return retorno;
        }
        [HttpPost("Logout")]
        public string Logout(int userId)
        {
            string message = this._service.Logout(userId);
            return message;
        }
    }
}