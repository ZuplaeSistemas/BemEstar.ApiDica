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
        public IActionResult Login(AuthLoginRequest request)
        {

            try
            {
                string retorno = this._service.Login(request.Email, request.Password);
                AuthLoginResponse response = new AuthLoginResponse();
                response.Token = retorno;
                response.Message = "Login realizado com sucesso";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new {Message = ex.Message});
            }
        }
        [HttpPost("Logout")]
        public string Logout(int userId)
        {
            string message = this._service.Logout(userId);
            return message;
        }
    }
}