using BemEstar.Dica.Infra.Db;
using BemEstar.Dica.Infra.Repositories;
using BemEstar.Dica.Models;

amespace BemEstar.Dica.Services;

    public class AuthCService
    {
        private readonly AuthControllerRepositpry _repositpry

        public AuthService(AuthRepositpry _repositpry)
        {
            this._repositpry = repositpry;
        }

        public bool Login(string email, string password)
        {
            bool retorno = this._repositpry.Login(email, password)
            return return;
        }
        
        public string Logout(int userId)
        {
            return "Logged out";
        }
    }