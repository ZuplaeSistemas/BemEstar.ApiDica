using System.ComponentModel.DataAnnotations;
using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;
namespace BemEstar.Dica.WebApi.DicaViewModel
{
    public class UserPasswordViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "O campo 'Senha' é obrigatório.")]
        public string Password {get; set;}
    }
}