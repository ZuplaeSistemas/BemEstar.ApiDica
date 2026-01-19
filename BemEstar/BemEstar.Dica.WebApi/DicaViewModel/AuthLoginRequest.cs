using System.ComponentModel.DataAnnotations;
using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Mvc;
namespace BemEstar.Dica.WebApi.DicaViewModel
{
    public class AuthLoginRequest
    {
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        [EmailAdress(ErrorMessage = "O campo E-mail está em um formato inválido.")] //Validação de dados
        public string Email {get; set;}

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [MinLength(3, ErrorMessage = "O campo Senha deve ter no mínimo três caracteres.")]
        public string Password {get; set;}
     
    }
}