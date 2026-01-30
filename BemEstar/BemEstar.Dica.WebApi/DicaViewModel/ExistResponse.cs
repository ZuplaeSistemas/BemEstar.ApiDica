using System.ComponentModel.DataAnnotations;
using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Mvc;
namespace BemEstar.Dica.WebApi.DicaViewModel
{
    public class ExistResponse
    {
        public int Id { get; set; }
        public bool Exist { get; set; }
    }
}