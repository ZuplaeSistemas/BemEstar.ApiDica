using System.ComponentModel.DataAnnotations;
using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;
namespace BemEstar.Dica.WebApi.DicaViewModel
{
    public class UserPutRequest
    {
        [Required(ErrorMessage = "A senha precisa ser preenchida")]
        [MinLength(3, ErrorMessage = "O campo senha deve ter no mínimo 3 caracteres.")]
        public string Password {get; set;}
    }
}