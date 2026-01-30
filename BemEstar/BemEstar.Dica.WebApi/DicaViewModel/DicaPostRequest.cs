using System.ComponentModel.DataAnnotations;
using BemEstar.Dica.Models;
using BemEstar.Dica.Services;
using Microsoft.AspNetCore.Mvc;
namespace BemEstar.Dica.WebApi.DicaViewModel
{
    public class DicaPostRequest
    {
        [Required("O campo Título é obrigatório.")]
        [MinLength(3, ErrorMessage = "O campo Título deve ter no mínimo 3 caracteres.")]
        public string Titulo { get; set; }
        [Required("O campo Descricao é obrigatório.")]
        [MinLength(3, ErrorMessage = "O campo Descricao deve ter no mínimo 3 caracteres.")]
        public string Descricao { get; set; }
        [Required("O campo Categoria é obrigatório.")]
        [MinLength(3, ErrorMessage = "O campo Categoria deve ter no mínimo 3 caracteres.")]
        public string Categoria { get; set; }

       // [Required("O campo BirthDate é obrigatório.")]
       // [DataType(DataType.Date, ErrorMessage = "O campo BirthDate deve ser uma data válida.")]
       // public DateTime BirthDate { get; set; }
    }
}