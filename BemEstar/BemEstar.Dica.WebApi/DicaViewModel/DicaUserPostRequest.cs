using System.ComponentModel.DataAnnotations;
using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Mvc;
namespace BemEstar.Dica.WebApi.DicaViewModel
{
    public class DicaUserPostRequest
    {
        [Required(ErrorMessage = "O e-mail precisa ser preenchido!")]
        [EmailAdress(ErrorMessage = "O campo e-mail está em formato inválido.")]
        public string Email {get; set;}

        [Required(ErrorMessage = "A senha precisa ser preenchida")]
        [MinLength(3, ErrorMessage = "O campo senha deve ter no mínimo 3 caracteres.")]
        public string Password {get; set;}
        
        [Required(ErrorMessage = "O id da Pessoa precisa ser preenchido")]
        [Range(1, int.MaxValue, ErrorMessage = "O id da Dica precisa ser maior que zero")]
        public int Dica_Id {get; set;}
    }
}