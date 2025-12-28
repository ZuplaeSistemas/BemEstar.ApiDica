using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Mvc;
namespace BemEstar.Dica.WebApi.DicaViewModel
{
    public class DicaUserViewModel : BaseViewModel
    {
        public string Email {get; set;}
       
        // Foreign Key para Person - POO Composição
        public Person Person {get; set;}
    }
}