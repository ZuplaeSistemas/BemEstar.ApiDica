using System.ComponentModel.DataAnnotations;
using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Mvc;
namespace BemEstar.Dica.WebApi.DicaViewModel
{
    public class DicaGetResponse : BaseViewModel
    {
        public string Titulo { get; set; };
        public string Descricao { get; set; };
        public string Categoria { get; set; };
    }
}